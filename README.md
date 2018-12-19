# GS-LOADER

## Objetivo

Desenvolver uma ferramenta para efetuar a instalação, atualização e carregamento de sistemas.

## Funções

### gs-loader

Programa de instalação, atualização e carregamento

- [x] Mostrar help
- [ ] Instalar um programa
  - [ ] A partir do arquivo de configuração
  - [ ] A partir de uma pasta
  - [ ] A partir de uma URL HTTP
- [ ] Executar programa
  - [x] Verificar se já está em execução
- [ ] Atualização
  - [ ] A partir deu uma pasta
  - [ ] Via HTTP
  - [ ] Via FTP
- [ ] Informações
  - [x] Mostrar informações sobre o processo corrente
  - [x] Registrar execuções
  - [ ] Registrar saídas inválidas
  - [ ] Produzir estatísticas

### gs-loader-setup

Programa de construção de setup

- [ ] Criar/editar arquivo de configuração


## Comandos

### Executar sistema

gsloader.exe --run:pasta

### Verificar sistema

gsloader.exe --verify:pasta

## OBJETIVO SECUNDÁRIO

Transformar este projeto dentro dos princípios SOLID. Links:[1](https://medium.com/thiago-aragao/solid-princ%C3%ADpios-da-programa%C3%A7%C3%A3o-orientada-a-objetos-ba7e31d8fb25), [2](http://www.eduardopires.net.br/2013/04/orientacao-a-objeto-solid>), [3](https://medium.com/@carloszan/entendendo-solid-com-exemplos-em-c-98a983d47f) 

- SSRP - Single responsibility principle - Princípio da Responsabilidade Única
> Uma classe deve ter um, e somente um, motivo para mudar.

- OOCP - Open/closed principle - Princípio do Aberto/Fechado
> Você deve ser capaz de estender um comportamento de uma classe sem a necessidade de modificá-lo.

- LLSP - Liskov substitution principle - Princípio da substituição de Liskov
> As classes derivadas devem ser substituíveis por suas classes bases.

- IISP - Interface segregation principle - Princípio da segregação de interfaces
> Muitas interfaces específicas são melhores do que uma interface única geral.

- DDIP - Dependency inversion principle - Princípio da inversão de dependência
> Dependa de abstrações e não de implementações.

## Entidades

FileEntry - Arquivo relacionado para cópia/atualização
