-- Define qual banco de dados será utilizado
USE InLock_Games;
GO

-- Listar todos os usuários
SELECT * FROM Usuarios

-- Listar todos os estúdios
SELECT * FROM Estudios; 

-- Listar todos os jogos
SELECT * FROM Jogos;

-- Listar todos os jogos e seus respectivos estúdios
SELECT J.IdJogo, J.NomeJogo, J.Descricao, J.DataLancamento, J.Valor, J.IdEstudio, E.NomeEstudio FROM Jogos J
INNER JOIN Estudios E
ON J.IdEstudio = E.IdEstudio;
GO

-- Buscar e trazer na lista todos os estúdios com os respectivos jogos, mesmo que um estúdio não tenha jogo
SELECT * FROM Estudios E
LEFT JOIN Jogos J
ON E.IdEstudio = J.IdEstudio;
GO

-- Buscar um usuário por e-mail e senha
SELECT * FROM Usuarios U
INNER JOIN TiposUsuario TU
ON U.IdTipoUsuario = TU.IdTipoUsuario
WHERE Email = 'admin@admin.com' AND Senha = 'admin';
GO

-- Buscar um jogo e seu respectivo estúdio por IdJogo
SELECT * FROM Jogos J
INNER JOIN Estudios E
ON J.IdEstudio = E.IdEstudio
WHERE J.IdJogo = 1;
GO

-- Buscar um estúdio com seus respectivos jogos pelo IdEstudio
SELECT * FROM Estudios E
INNER JOIN Jogos J
ON E.IdEstudio = J.IdEstudio
WHERE E.IdEstudio = 1;
GO

-- Lista os jogos de um determinado estúdio
SELECT * FROM Jogos J
INNER JOIN Estudios E
ON J.IdEstudio = E.IdEstudio
WHERE E.IdEstudio = 1;
GO