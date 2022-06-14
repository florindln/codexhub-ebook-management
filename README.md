# codexhub-ebook-management

Full stack project about an ebook management website. It features similarities to the popular goodreads website.
This project's back-end is written using a microservices architecture with ASP.NET Core. The front-end is written using ReactJS.

Noteworthy functionality:
- Ebook filtering
- Recommendation system
- Rating system
- Ebook and user management
- Using API's to pull data
- Authentication/Authorization
- Fully responsive front-end

If running locally:

- BookService appsettings.json -> mysql server=localhost port=3007, redis =localhost
- Inventory and Users -> MongoDbSettings Host=localhost
- React .env -> change https to http for the gateway url
- Ocelot Gateway -> use as ocelot.json the one with localhosts

If running everything in docker-compose

- BookService appsettings.json -> server=mysqldb port=3006, redis=rediscache
- Inventory and Users -> MongoDbSettings Host=mongodb
- React .env -> change http to https for the gateway url
- Ocelot Gateway -> use as ocelot.json the one the service names
