# User API Dev Guide

API follows Clean Architecture and implements CQRS pattern with help of Mediatr

## Building

Instead of using a docker image for SQL Server, i took the liberty to use actual SQL instance already avilable on my system
Code however will work well with both docker sql image or an actual sql server

I attempted to build all important features except List Users and Accounts, these can be implemented using PagedResponse approach

## Testing

Integration tests for user endpoint are included, tests for account end point will be similar

## Deploying

## Additional Information
To Do : Include swagger, logging and health checks
