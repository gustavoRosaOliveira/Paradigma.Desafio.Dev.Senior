# Tarefa 2

## Descrição
* Consulta SQL para Salários Mais Altos por Departamento

## Consulta

```sql
SELECT D.Nome AS Departamento, P.Nome AS Pessoa, P.Salario AS Salario
FROM Pessoa P
JOIN Departamento D ON P.DeptId = D.Id
WHERE P.Salario = (
    SELECT MAX(Salario)
    FROM Pessoa
    WHERE DeptId = P.DeptId
);
```

## Bônus: caso queiram validar o funcionamento da query criei um servidor MySQL de banco de dados, um banco de dados com os dados para teste e uma view que contém o SELECT (vide dados abaixo).

* Host: sql10.freesqldatabase.com
* Database name: sql10737166
* Database user: sql10737166
* Database password: VEtua4Yh7L
* Port number: 3306

* Comando para exectuar a view: 
```sql
SELECT * FROM SalarioMaisAltoPorDepartamento
```
