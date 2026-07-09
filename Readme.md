Тестовое задание для TrueCode.

Реализован микросервисный проект на .NET 8 с использованием Clean Architecture, CQRS, JWT-аутентификации, фонового сервиса обновления курсов валют ЦБ РФ, PostgreSQL и API Gateway на Ocelot.

## Используемые технологии

- C#
- .NET 8
- ASP.NET Core Web API
- PostgreSQL
- Entity Framework Core
- Clean Architecture
- CQRS
- MediatR
- JWT Authentication
- Ocelot API Gateway
- xUnit
- Moq

---

# Архитектура

Проект состоит из нескольких сервисов:

- **MigrationService** — создание и применение миграций БД
- **UserService** — регистрация, авторизация и работа с пользователями
- **FinanceService** — получение курсов валют пользователя
- **CurrencyUpdater** — фоновый сервис обновления курсов валют из ЦБ РФ
- **ApiGateway** — маршрутизация запросов через Ocelot

---

# Возможности

## UserService

- Регистрация пользователя
- Авторизация (JWT)
- Logout
- Хэширование паролей (BCrypt)

## FinanceService

- Получение избранных валют пользователя
  
- ## CurrencyUpdater

- Получение XML с сайта ЦБ РФ
- Автоматическое обновление курсов валют
- Интервал обновления задается в appsettings.json

 
 # Запуск проекта

## 1. Создать базу PostgreSQL

Например:

```
Database: trueCode
```

---

## 2. Настроить строку подключения

Во всех сервисах (UserService.Api, FinanceService.Api, CurrencyUpdater, MigrationService) указать:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=YOU_HOST;Port=****;Database=trueCode;Username=YOU_USERNAME;Password=YOUR_PASSWORD"
  }
}
```

---

## 3. Применить миграции

Запустить проект:

```
MigrationService
```

---

## 4. Запустить сервисы

- UserService.API
- FinanceService.API
- CurrencyUpdater
- ApiGateway

---

# API

## Регистрация

POST

```
/api/auth/register
```

Body

```json
{
  "name": "UserName",
  "password": "123456"
}
```

---

## Авторизация

POST

```
/api/auth/login
```

Body

```json
{
  "name": "UserName",
  "password": "123456"
}
```

Ответ

```json
{
  "token": "jwt_token" // тут придёт токен
}
```

---

## Получение списка валют

GET

```
/api/finance/currencies
```

Требуется JWT Bearer Token.
