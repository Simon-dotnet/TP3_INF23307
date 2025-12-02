Connexion à la BD:

Il faut avoir un serveur SQL avec les infos suivantes:
<img width="962" height="667" alt="image" src="https://github.com/user-attachments/assets/b8bab5da-e48a-470f-a579-9e37ef33bdef" />
Le mot de passe est admin123*
Il faut appeler la BD: "nordikaventure"

Il faut suivre précisément cette connexion car notre programme va essayer de se connecter à celui ci depuis ce fichier:
<img width="1005" height="367" alt="image" src="https://github.com/user-attachments/assets/e00a984f-a7d8-4b42-98ce-ad8df7b0c615" />

En cas de soucis, faire ces étapes:
- Rouler cette commande dans SQL:
 ```SET FOREIGN_KEY_CHECKS = 0;

SET @tables = NULL;
SELECT GROUP_CONCAT('`', table_name, '`') INTO @tables
FROM information_schema.tables
WHERE table_schema = DATABASE();

SET @sql = CONCAT('DROP TABLE IF EXISTS ', @tables, ';');
PREPARE stmt FROM @sql;
EXECUTE stmt;nordikaventure
DEALLOCATE PREPARE stmt;

SET FOREIGN_KEY_CHECKS = 1;
```
- Supprimer le dossier de Migrations dans le code
- Faire ```dotnet ef migrations add initial-migration```
- Puis ```dotnet ef database update```

Puis lancer l'application, ceci devrais recréer la BD, puis la repeupler

Les identifiants sont les suivants:
<img width="1026" height="130" alt="image" src="https://github.com/user-attachments/assets/6793bf86-2ef4-47d5-8354-ce207be1f5f4" />

Permissions :

Manager - Tous les modules

Accountant - Module finance seulement

Employee - Module stock seulement

