# Simulador SO (Sistema Operacional)

Projeto feito em c# para simular um sistema operacional para cadeira de Sistemas Operacionais

# O que é necessário parar conseguir rodar o projeto

[ ] Ter o SDK do c# instalado na máquina
[ ] Ter o c# devkit instalado no vscode ( caso esteja rodando no vscode, em outras IDEs como rider e VsCommunity não é necessário)

Exemplo de output no terminal

```
SO> create chrome
SO> create vscode
SO> create spotify
SO> list
PID | Nome       | CPU | MEM | PRIO | Estado
1   | chrome     | 5   | 92  | 3    | Pronto
2   | vscode     | 2   | 76  | 1    | Pronto
3   | spotify    | 4   | 120 | 5    | Pronto

SO> run prio
Executando por Prioridade...
-> Executando vscode (PID 2) - CPU restante: 2
✓ Processo 2 finalizado!
-> Executando chrome (PID 1) - CPU restante: 5
```
