Pessoal, comentei o máximo que eu pude em cada arquivo do projeto Web Service para vocês, peço que vocês olhem os aquivos ao abrir o projeto na ordem que citarei aqui, por pastas.

1 - services
2 - models
3 - helpers
4 - entities
5 - controllers


Peço que olhem o Webcofig, mudem a string de conexão, deixei pronta ali, só colocar os dados de acesso ao banco de vocês, e então só utilizar conforme a pasta entities para buscar os dados do banco.

No WebConfig vocês tem de procurar o que tem de "IMapNat" e trocar pelo nome da interface de vocês, basta substituir, para o WebService funcionar.(Podem deixar isto por último).

Vocês também iram ver que todos os método são do tipo ResponseLocais(Que implementa meu model) o de vocês também será assim só que com o nome do de vocês Ex: ResponsePessoa(Que contém uma List do model Pessoa), exatamente como está hoje só que com as classes e objetos de vocês.

Qualquer dúvida só me mandar e-mail.