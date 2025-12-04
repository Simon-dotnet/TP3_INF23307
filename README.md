# Nordik Aventure – Guide d’installation

Ce projet utilise Docker pour simplifier l’installation.
Aucune configuration manuelle de MySQL ou .NET n’est nécessaire.

## 1. Prérequis

Installer Docker Desktop :
```console
https://www.docker.com/products/docker-desktop/
```
Ouvrir Docker Desktop et le laisser fonctionner.

## 2. Récupérer le projet

Le projet se trouve sur GitHub :
```console
https://github.com/Simon-dotnet/TP3_INF23307.git
```
Il faut seulement clôner :
```console
git clone https://github.com/Simon-dotnet/TP3_INF23307.git
```
Ou en utilisant un outil tel que GitHub Desktop.

## 3. Démarrer l’application

1. Ouvrir un terminal dans le dossier du projet clôné:
```console
cd [path]/TP3_INF23307/
```
2. Lancer les commandes suivantes (toujours sur le même terminal):
```console
docker compose build
docker compose up
```

## 4. Accéder au site

Quand les conteneurs sont démarrés, ouvrir un navigateur :
```console
http://localhost:8080
```
Cela affiche l’écran de connexion.

## 5. Comptes disponibles

| Rôle        | Courriel              | Mot de passe |
|-------------|------------------------|--------------|
| Employé     | marc123@gmail.com      | marc123*     |
| Gestionnaire| jean123@gmail.com      | jean123*     |
| Comptable   | money@gmail.com        | money123*    |

**Permissions**  
Gestionnaire/Manager : accès complet  
Comptable/Accountant : module finance seulement  
Employé/Employee : module stock seulement


## 6. Informations sur MySQL (si Docker ne fonctionne pas)

Pour se connecter manuellement à la base MySQL (si Docker ne fonctionne pas) :
- Hôte : localhost
- Port : 3307
- Base : nordikaventure
- Utilisateur : root
- Mot de passe : admin123*

## 7. Arrêter l’application

Dans le terminal qui exécute Docker :
```console
Ctrl + C
```
Puis :
```console
docker compose down
```
Pour supprimer aussi les données :
```console
docker compose down -v
```

## 8. Problèmes fréquents

### Le port 8080 est occupé

Modifier docker-compose.yml :
```docker
ports: - "8090:8080"
```
Accéder ensuite via :
```console
http://localhost:8090
```

### Le port 3307 pose problème

Changer la ligne :
```console
ports: - "3308:3306"
```

----------------------------------------------------------------------
## ANCIEN GUIDE (SI L'AUTRE N'EST PAS FONCTIONNEL) :
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

