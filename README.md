# codexhub-ebook-management

If running locally:

- BookService appsettings.json -> mysql server=localhost port=3007, redis =localhost
- Inventory and Users -> MongoDbSettings Host=localhost
- React .env -> change https to http for the gateway url
- Ocelot Gateway -> use as ocelot.json the one with localhosts

If running everything in docker

- BookService appsettings.json -> server=mysqldb port=3006, redis=rediscache
- Inventory and Users -> MongoDbSettings Host=mongodb
- React .env -> change http to https for the gateway url
- Ocelot Gateway -> use as ocelot.json the one the service names
