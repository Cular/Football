# Перед началом работы необходимо подготовить БД. (MS SQL):

  - Сбилдить проект.
  - [Запустить-ef-migration](#Запустить-ef-migration).
  - Запустить проект.

## #Запустить-ef-migration!

Выполнить в cmd.exe:
```sh
 cd ~\Football\Data\Data.DataBaseContext
 dotnet ef database update
```
Подробней: https://docs.microsoft.com/ru-ru/ef/core/managing-schemas/migrations/