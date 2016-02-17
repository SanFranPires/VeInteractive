Introduction
============

This project was developed within the scope of a technical test.

Used Technologies
-----------------

* .NET 4.5.2 (C#)
* Visual Studio 2015 Community Edition
* ServiceStack
* Redis
* ManyConsole
* NUnit

Project Notes
=============

Requirements
------------

The REST services use Redis as the storage back-end for the One-Time Passwords. For that reason, a running instance of Redis server must be available. The endpoint settings for the Redis server can be configured in the *Web.config* file, in the `RedisConnectionString` application setting.

Usage
-----

The solution contains a server RESTful API and a client application.

### RESTful API

The main project `VeInteractive.TechTest.OneTimePassword` should be deployed on an IIS server.

### Client application

The project `VeInteractive.TechTest.OneTimePassword.Client` contains a console application that allows exercising the RESTful API. There are two actions that can be performed with this console application:
* Create a new one-time password using the arguments `create <userId>`. This will print out the created password on the console.
* Validate the one-time password using the arguments `authenticate <userId> <password>`. This will print out whether the authentication was successful or not.

In both actions it is possible to specify the API endpoint with the following optional arguments:
* `h|host=<hostname>`: defaults to localhost.
* `p|port=<portNumber>`: defaults to 80.

Author
======

This code was developed by Francisco Pires.
