Pessoal, comentei o m�ximo que eu pude em cada arquivo do projeto Web Service para voc�s, pe�o que voc�s olhem os aquivos ao abrir o projeto na ordem que citarei aqui, por pastas.

1 - services
2 - models
3 - helpers
4 - entities
5 - controllers


Pe�o que olhem o Webcofig, mudem a string de conex�o, deixei pronta ali, s� colocar os dados de acesso ao banco de voc�s, e ent�o s� utilizar conforme a pasta entities para buscar os dados do banco.

No WebConfig voc�s tem de procurar o que tem de "IMapNat" e trocar pelo nome da interface de voc�s, basta substituir, para o WebService funcionar.(Podem deixar isto por �ltimo).

Voc�s tamb�m iram ver que todos os m�todo s�o do tipo ResponseLocais(Que implementa meu model) o de voc�s tamb�m ser� assim s� que com o nome do de voc�s Ex: ResponsePessoa(Que cont�m uma List do model Pessoa), exatamente como est� hoje s� que com as classes e objetos de voc�s.

Qualquer d�vida s� me mandar e-mail.