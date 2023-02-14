# Introductory task - Meetup API
[![Build and Test](https://github.com/kaallaash/Meetup/actions/workflows/sonarCloud_BE.yaml/badge.svg)](https://github.com/kaallaash/Meetup/actions/workflows/sonarCloud_BE.yaml/badge.svg)

###Описание
Разработка CRUD Web API для работы с мероприятиями (создание, изменение, удаление,
получение), выполняется на .Net Core, с использованием EF Core.
Должна прилагаться инструкция по запуску проекта.

###Функционал Web API
1. Получение списка всех событий;
2. Получение определённого события по его Id;
3. Регистрация нового события;
4. Изменение информации о существующем событии;
5. Удаление события.

###Информация о событии
1. Название / тема;
2. Описание, план;
3. Организатор, спикер;
4. Время и место проведения.

###Необходимые к использованию технологии
1. .Net 5.0+;
2. Entity Framework Core;
3. MS SQL / PostgreSQL or any other;
4. AutoMapper / Mapster or any other;
5. Authentication via bearer token (ex.: IdentityServer4);
6. Swagger.

###Архитектура
При разработке приложения не забывайте про архитектуру! (microservices/n-layer/cqrs…..)