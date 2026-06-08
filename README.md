# S³A — Sondas Sísmicas Autônomas

## Integrantes

- Camila Mie Takara - RM555418
- Guilherme Barbiero - RM555185
- Marco Antonio Gonçalves - RM556818
- Matheus Cantiere - RM558479
- Vinicius Castro - RM556137


## A ideia do projeto S³A
Hoje a exploração planetária depende quase exclusivamente de rovers — os
carrinhos robóticos da NASA como o Curiosity e o Perseverance. O problema
é que rovers são vulneráveis: podem atolar no solo, perder energia com
tempestades de poeira e, quando isso acontece, a missão inteira pode ser
comprometida.

O projeto S³A propõe uma abordagem diferente: em vez de um único rover
frágil e caro, uma rede de dezenas de mini-droids baratos e independentes
é dispersada sobre uma região de Marte ou da Lua. Cada mini-droid desce
em trajetória balística e, ao tocar o solo, dispara um mecanismo que finca
uma haste metálica no regolito — ele vira uma estação científica estacionária.

A partir desse momento, cada droid usa dois tipos de sensor:

- **Sismômetro de banda larga** — escuta vibrações do solo (marsquakes e
  moonquakes) para detectar cavidades, tubos de lava e aquíferos subterrâneos
  a profundidades de até 2,5 km.
- **Sensor óptico** — emite feixes laser e analisa reflexão e refração para
  identificar minerais e indícios de gelo ou água logo abaixo do ponto de
  ancoragem.

Os dados de todos os droids são transmitidos à sonda-mãe, que os repassa
via Deep Space Network para equipes científicas na Terra. Se um droid
detecta uma cavidade provável, ativa automaticamente um módulo de
tomografia sísmica para gerar uma imagem 3D do subsolo.

A proposta resolve três limitações dos rovers atuais: mobilidade
(os droids não precisam se mover), redundância (se um falha, os outros
continuam) e custo (unidades pequenas e replicáveis substituem uma única
máquina de alto valor).


## Como o projeto em C# se integra ao S³A

O sistema em C# simula o **centro de controle em Terra** da missão S³A.
É o software que recebe os dados dos droids, analisa os resultados e
persiste tudo em banco de dados — exatamente o que uma equipe da NASA
ou da ESA faria em uma missão real.

| Código C# | Representa na missão |
|---|---|
| Classe abstrata `Sensor` | Qualquer sensor embarcado num droid |
| `SensorSismico` e `SensorOptico` | Os dois sensores de cada droid |
| `LeituraSismica` e `LeituraOptica` | Os pacotes de dados transmitidos à Terra |
| `MiniDroid` | unidade droid fincada no solo de um corpo celeste |
| `struct CoordenadaPlanetaria` | A posição exata onde o droid foi fincado |
| `struct Alerta` | Notificação gerada quando algo relevante é detectado |
| `IAnalisador` | Contrato do serviço científico de análise de dados |
| `AnalisadorGeofisico` | O algoritmo que interpreta os dados sísmicos e ópticos |
| `ITransmissor` | Contrato de envio de dados via Deep Space Network |
| `BancoDados` | Persistência dos dados recebidos no servidor em Terra |
| Exceções customizadas | Falhas reais: sensor inativo, droid sem energia, etc. |

## Banco de dados

Duas tabelas criadas automaticamente no SQL Server LocalDB:

- **Leituras** — cada leitura sísmica e óptica coletada pelos droids
- **Alertas** — alertas gerados pela análise geofísica automática
