# Overview

This method relies on

1. Overriding `SqlServerQuerySqlGeneratorFactory`. 
2. (Ab)using Query Tags to pass required per-query information to sql generator.

See Startup.cs for required configuration and sample usage. 
