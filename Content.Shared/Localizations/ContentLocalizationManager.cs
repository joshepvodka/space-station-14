using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Robust.Shared.Utility;
using Robust.Shared.GameObjects.Components.Localization;
using Robust.Shared.Enums;

namespace Content.Shared.Localizations
{
    public sealed class ContentLocalizationManager
    {
        [Dependency] private readonly ILocalizationManager _loc = default!;
        [Dependency] private readonly IEntityManager _entMan = default!;

        // If you want to change your codebase's language, do it here.
        private const string Culture = "pt-BR";

        /// <summary>
        /// Custom format strings used for parsing and displaying minutes:seconds timespans.
        /// </summary>
        public static readonly string[] TimeSpanMinutesFormats = new[]
        {
            @"m\:ss",
            @"mm\:ss",
            @"%m",
            @"mm"
        };

        public void Initialize()
        {
            var culture = new CultureInfo(Culture);

            _loc.LoadCulture(culture);
            _loc.AddFunction(culture, "PRESSURE", FormatPressure);
            _loc.AddFunction(culture, "POWERWATTS", FormatPowerWatts);
            _loc.AddFunction(culture, "POWERJOULES", FormatPowerJoules);
            _loc.AddFunction(culture, "UNITS", FormatUnits);
            _loc.AddFunction(culture, "TOSTRING", args => FormatToString(culture, args));
            _loc.AddFunction(culture, "LOC", FormatLoc);
            _loc.AddFunction(culture, "NATURALFIXED", FormatNaturalFixed);
            _loc.AddFunction(culture, "NATURALPERCENT", FormatNaturalPercent);

            //  Localizações pt-BR

            _loc.AddFunction(culture, "ARTIGO", FormatArtigo);
            _loc.AddFunction(culture, "ARTINDEF", FormatArtigoIndefinido);
            _loc.AddFunction(culture, "PRONOME", FormatPronome);
            _loc.AddFunction(culture, "PRODEM", FormatPronomeDemonstrativo);
            _loc.AddFunction(culture, "DE", FormatPreposicaoPosse);

            /*
             * The following language functions are specific to the english localization. When working on your own
             * localization you should NOT modify these, instead add new functions specific to your language/culture.
             * This ensures the english translations continue to work as expected when fallbacks are needed.
             */
            var cultureEn = new CultureInfo("pt-BR");

            _loc.AddFunction(cultureEn, "MAKEPLURAL", FormatMakePlural);
            _loc.AddFunction(cultureEn, "MANY", FormatMany);
        }

        // Substitui a função pelo artigo correto para o genero da entidade.
        private ILocValue FormatArtigo(LocArgs args)
        {
            if (args.Args.Count < 1) return new LocValueString(nameof(Gender.Neuter));

            ILocValue entity0 = args.Args[0];
            if (entity0.Value != null)
            {
                EntityUid entity = (EntityUid)entity0.Value;

                if(_entMan.TryGetComponent(entity, out GrammarComponent? grammar) && grammar.Gender.HasValue)
                {
                    return EscolherArtigo(grammar.Gender.Value);
                }
            }

            return EscolherArtigo(Gender.Female);
        }

        private LocValueString EscolherArtigo(Gender gender) => gender switch
        {
            Gender.Neuter => new LocValueString("e"),
            Gender.Epicene => new LocValueString("e"),
            Gender.Female => new LocValueString("a"),
            Gender.Male => new LocValueString("o"),
            _ => new LocValueString("a")
        };
        
        private ILocValue FormatArtigoIndefinido(LocArgs args)
        {
            if (args.Args.Count < 1) return new LocValueString(nameof(Gender.Neuter));

            ILocValue entity0 = args.Args[0];
            if (entity0.Value != null)
            {
                EntityUid entity = (EntityUid)entity0.Value;

                if(_entMan.TryGetComponent(entity, out GrammarComponent? grammar) && grammar.Gender.HasValue)
                {
                    return EscolherArtigoIndefinido(grammar.Gender.Value);
                }
            }

            return EscolherArtigoIndefinido(Gender.Female);
        }

        private LocValueString EscolherArtigoIndefinido(Gender gender) => gender switch
        {
            Gender.Neuter => new LocValueString("ume"),
            Gender.Epicene => new LocValueString("ume"),
            Gender.Female => new LocValueString("uma"),
            Gender.Male => new LocValueString("um"),
            _ => new LocValueString("uma")
        };
        
        // Substitui a função pelo pronome correto para o genero da entidade.
        private ILocValue FormatPronome(LocArgs args)
        {
            if (args.Args.Count < 1) return new LocValueString(nameof(Gender.Neuter));

            ILocValue entity0 = args.Args[0];
            if (entity0.Value != null)
            {
                EntityUid entity = (EntityUid)entity0.Value;

                if(_entMan.TryGetComponent(entity, out GrammarComponent? grammar) && grammar.Gender.HasValue)
                {
                    return EscolherPronome(grammar.Gender.Value);
                }
            }

            return EscolherPronome(Gender.Epicene);
        }

        private LocValueString EscolherPronome(Gender gender) => gender switch
        {
            Gender.Neuter => new LocValueString("isso"),
            Gender.Epicene => new LocValueString("elu"),
            Gender.Female => new LocValueString("ela"),
            Gender.Male => new LocValueString("ele"),
            _ => new LocValueString("ela")
        };
        
        private ILocValue FormatPronomeDemonstrativo(LocArgs args)
        {
            if (args.Args.Count < 1) return new LocValueString(nameof(Gender.Neuter));

            ILocValue entity0 = args.Args[0];
            if (entity0.Value != null)
            {
                EntityUid entity = (EntityUid)entity0.Value;

                if(_entMan.TryGetComponent(entity, out GrammarComponent? grammar) && grammar.Gender.HasValue)
                {
                    return EscolherPronomeDemonstrativo(grammar.Gender.Value);
                }
            }

            return EscolherPronomeDemonstrativo(Gender.Epicene);
        }

        private LocValueString EscolherPronomeDemonstrativo(Gender gender) => gender switch
        {
            Gender.Neuter => new LocValueString("disso"),
            Gender.Epicene => new LocValueString("delu"),
            Gender.Female => new LocValueString("dela"),
            Gender.Male => new LocValueString("dele"),
            _ => new LocValueString("dela")
        };

        // Substitui a função pela preposição de posse (de, da, do) correto para o genero da entidade.
        private ILocValue FormatPreposicaoPosse(LocArgs args)
        {
            if (args.Args.Count < 1) return new LocValueString(nameof(Gender.Neuter));

            ILocValue entity0 = args.Args[0];
            if (entity0.Value != null)
            {
                EntityUid entity = (EntityUid)entity0.Value;

                if(_entMan.TryGetComponent(entity, out GrammarComponent? grammar) && grammar.Gender.HasValue)
                {
                    return EscolherPreposicaoPosse(grammar.Gender.Value);
                }
            }

            return EscolherPreposicaoPosse(Gender.Epicene);
        }

        private LocValueString EscolherPreposicaoPosse(Gender gender) => gender switch
        {
            Gender.Neuter => new LocValueString("de"),
            Gender.Epicene => new LocValueString("de"),
            Gender.Female => new LocValueString("da"),
            Gender.Male => new LocValueString("do"),
            _ => new LocValueString("de")
        };

        private ILocValue FormatMany(LocArgs args)
        {
            var count = ((LocValueNumber) args.Args[1]).Value;

            if (Math.Abs(count - 1) < 0.0001f)
            {
                return (LocValueString) args.Args[0];
            }
            else
            {
                return (LocValueString) FormatMakePlural(args);
            }
        }

        private ILocValue FormatNaturalPercent(LocArgs args)
        {
            var number = ((LocValueNumber) args.Args[0]).Value * 100;
            var maxDecimals = (int)Math.Floor(((LocValueNumber) args.Args[1]).Value);
            var formatter = (NumberFormatInfo)NumberFormatInfo.GetInstance(CultureInfo.GetCultureInfo(Culture)).Clone();
            formatter.NumberDecimalDigits = maxDecimals;
            return new LocValueString(string.Format(formatter, "{0:N}", number).TrimEnd('0').TrimEnd('.') + "%");
        }

        private ILocValue FormatNaturalFixed(LocArgs args)
        {
            var number = ((LocValueNumber) args.Args[0]).Value;
            var maxDecimals = (int)Math.Floor(((LocValueNumber) args.Args[1]).Value);
            var formatter = (NumberFormatInfo)NumberFormatInfo.GetInstance(CultureInfo.GetCultureInfo(Culture)).Clone();
            formatter.NumberDecimalDigits = maxDecimals;
            return new LocValueString(string.Format(formatter, "{0:N}", number).TrimEnd('0').TrimEnd('.'));
        }

        private static readonly Regex PluralEsRule = new("^.*(s|sh|ch|x|z)$");

        private ILocValue FormatMakePlural(LocArgs args)
        {
            var text = ((LocValueString) args.Args[0]).Value;
            var split = text.Split(" ", 1);
            var firstWord = split[0];
            if (PluralEsRule.IsMatch(firstWord))
            {
                if (split.Length == 1)
                    return new LocValueString($"{firstWord}es");
                else
                    return new LocValueString($"{firstWord}es {split[1]}");
            }
            else
            {
                if (split.Length == 1)
                    return new LocValueString($"{firstWord}s");
                else
                    return new LocValueString($"{firstWord}s {split[1]}");
            }
        }

        // TODO: allow fluent to take in lists of strings so this can be a format function like it should be.
        /// <summary>
        /// Formats a list as per english grammar rules.
        /// </summary>
        public static string FormatList(List<string> list)
        {
            return list.Count switch
            {
                <= 0 => string.Empty,
                1 => list[0],
                2 => $"{list[0]} and {list[1]}",
                _ => $"{string.Join(", ", list.GetRange(0, list.Count - 1))}, and {list[^1]}"
            };
        }

        private static ILocValue FormatLoc(LocArgs args)
        {
            var id = ((LocValueString) args.Args[0]).Value;

            return new LocValueString(Loc.GetString(id, args.Options.Select(x => (x.Key, x.Value.Value!)).ToArray()));
        }

        private static ILocValue FormatToString(CultureInfo culture, LocArgs args)
        {
            var arg = args.Args[0];
            var fmt = ((LocValueString) args.Args[1]).Value;

            var obj = arg.Value;
            if (obj is IFormattable formattable)
                return new LocValueString(formattable.ToString(fmt, culture));

            return new LocValueString(obj?.ToString() ?? "");
        }

        private static ILocValue FormatUnitsGeneric(LocArgs args, string mode)
        {
            const int maxPlaces = 5; // Matches amount in _lib.ftl
            var pressure = ((LocValueNumber) args.Args[0]).Value;

            var places = 0;
            while (pressure > 1000 && places < maxPlaces)
            {
                pressure /= 1000;
                places += 1;
            }

            return new LocValueString(Loc.GetString(mode, ("divided", pressure), ("places", places)));
        }

        private static ILocValue FormatPressure(LocArgs args)
        {
            return FormatUnitsGeneric(args, "zzzz-fmt-pressure");
        }

        private static ILocValue FormatPowerWatts(LocArgs args)
        {
            return FormatUnitsGeneric(args, "zzzz-fmt-power-watts");
        }

        private static ILocValue FormatPowerJoules(LocArgs args)
        {
            return FormatUnitsGeneric(args, "zzzz-fmt-power-joules");
        }

        private static ILocValue FormatUnits(LocArgs args)
        {
            if (!Units.Types.TryGetValue(((LocValueString) args.Args[0]).Value, out var ut))
                throw new ArgumentException($"Unknown unit type {((LocValueString) args.Args[0]).Value}");

            var fmtstr = ((LocValueString) args.Args[1]).Value;

            double max = Double.NegativeInfinity;
            var iargs = new double[args.Args.Count - 1];
            for (var i = 2; i < args.Args.Count; i++)
            {
                var n = ((LocValueNumber) args.Args[i]).Value;
                if (n > max)
                    max = n;

                iargs[i - 2] = n;
            }

            if (!ut.TryGetUnit(max, out var mu))
                throw new ArgumentException("Unit out of range for type");

            var fargs = new object[iargs.Length];

            for (var i = 0; i < iargs.Length; i++)
                fargs[i] = iargs[i] * mu.Factor;

            fargs[^1] = Loc.GetString($"units-{mu.Unit.ToLower()}");

            // Before anyone complains about "{"+"${...}", at least it's better than MS's approach...
            // https://docs.microsoft.com/en-us/dotnet/standard/base-types/composite-formatting#escaping-braces
            //
            // Note that the closing brace isn't replaced so that format specifiers can be applied.
            var res = String.Format(
                fmtstr.Replace("{UNIT", "{" + $"{fargs.Length - 1}"),
                fargs
            );

            return new LocValueString(res);
        }
    }
}
