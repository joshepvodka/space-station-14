# Strings for various entity state & client-side PVS related commands

cmd-reset-ent-help = Uso: resetent <ID da Entidade>
cmd-reset-ent-desc = Reseta a entidade para o último estado recebido do servidor. Isso também irá resetar entidades que foram removidas no null-space.
cmd-reset-all-ents-help = Uso: resetallents
cmd-reset-all-ents-desc = Reseta todas as entidades para o último estado recebido do servidor. Isso afeta apenas entidades que não foram removidas no null-space.
cmd-detach-ent-help = Uso: detachent <ID da Entidade>
cmd-detach-ent-desc = Remove a entidade no null-space, como se tivesse saído da zona de ação do PVS.
cmd-local-delete-help = Uso: localdelete <ID da Entidade>
cmd-local-delete-desc = Deleta a entidade. Ao contrário do comando delete normal, este comando opera no lado do cliente. Se a entidade não for do lado do cliente, isso provavelmente resultará em erros.
cmd-full-state-reset-help = Uso: fullstatereset
cmd-full-state-reset-desc = Reseta todas as informações de estado da entidade e solicita o estado completo do servidor.
