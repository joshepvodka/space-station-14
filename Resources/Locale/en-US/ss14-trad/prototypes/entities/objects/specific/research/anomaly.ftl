ent-AnomalyScanner = anomaly scanner
    .desc = A hand-held scanner built to collect information on various anomalous objects.
ent-AnomalyLocatorUnpowered = anomaly locator
    .desc = A device designed to aid in the locating of anomalies. Did you check the gas miners?
    .suffix = Unpowered
ent-AnomalyLocator = { ent-['AnomalyLocatorUnpowered', 'PowerCellSlotSmallItem'] }

  .suffix = Powered
  .desc = { ent-['AnomalyLocatorUnpowered', 'PowerCellSlotSmallItem'].desc }
ent-AnomalyLocatorEmpty = { ent-AnomalyLocator }
    .suffix = Empty
    .desc = { ent-AnomalyLocator.desc }
ent-AnomalyLocatorWideUnpowered = wide-spectrum anomaly locator
    .desc = A device that looks for anomalies from an extended distance, but has no way to determine the distance to them.
    .suffix = Unpowered
ent-AnomalyLocatorWide = { ent-['AnomalyLocatorWideUnpowered', 'PowerCellSlotSmallItem'] }

  .suffix = Powered
  .desc = { ent-['AnomalyLocatorWideUnpowered', 'PowerCellSlotSmallItem'].desc }
ent-AnomalyLocatorWideEmpty = { ent-AnomalyLocatorWide }
    .suffix = Empty
    .desc = { ent-AnomalyLocatorWide.desc }
ent-WeaponGauntletGorilla = G.O.R.I.L.L.A. gauntlet
    .desc = A robust piece of research equipment. When powered with an anomaly core, a single blow can launch anomalous objects through the air.
