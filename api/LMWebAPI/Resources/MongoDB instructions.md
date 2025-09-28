# abba_lm Database

_abba_lm_ is a mock Mongo database that can be used for tests or as a base template.

The collections are bound by the schemas in the MongoSchemas folder.

To import the mock database into your own MongoDB, extract the abba_lm.7z and use the _mongorestore_ command, replacing
the --drop path with the folder where abba_lm was extracted to

```
mongorestore --uri="mongodb://username:password@localhost:27017" --db=db_name path\to\dumpfolder 
```