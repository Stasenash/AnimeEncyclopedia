# AnimeEncyclopedia ASP.NET Project

* Тема: Энциклопедия аниме 
* Аналоги: https://shikimori.one/, http://www.world-art.ru/animation/, https://aniwiki.net/
* Описание: Веб-сайт с тайтлами аниме c возможностью регистрации и сохранения в избранное просмотренных/понравившихся/не понравившихся аниме.
* Доп. использование: https://jikan.moe/ Jikan-API для получения данных аниме-тайтлов


## Содержание

#### [Jikan API описание и документация](#jikan-api)
#### [Стек технологий](#ts-desc)
#### [Выполненные требования](#req)


----------

### Jikan API описание и документация
<a name="jikan-api"></a>
[Jikan](https://jikan.moe/)
(時間) открытый исходный код PHP и REST API для «самого активного онлайн-сообщества и базы данных аниме + манги» - MyAnimeList.net. Он анализирует веб-сайт, чтобы предоставить API, которого нет у MyAnimeList.

Узнать больше [документация](https://jikan.docs.apiary.io/)

----------

### Стек технологий
<a name="ts-desc"></a>

 * ASP.NET
 * Jikan API
 * MSSQL
 * EntityFramework
 * Serilog

----------

### Выполненные требования
<a name="req"></a>

* Регистрация пользователя
* Аутентификация
* Личный кабинет (страница с информацией)
* Использование СУБД MSSQL
* Миграция и обновление версий СУБД при старте приложения
* Журналирования запросов и типовых операций
* Использование Middleware для логирования запросов
* Одна общая страницы разметки (Layout)
* Файл конфигурации
* Шедулер
* Скрипт для билда проекта в релизную версию (.bat)
