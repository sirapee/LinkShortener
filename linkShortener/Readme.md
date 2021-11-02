#Link Shortener

A URL shortening api where the calling client passes the full URL such as https://indicina.co in a post request and it returns a short URL such as http://short.est/GeAi9K. Passing the shortened URL with return
the long URL. Using the example above, visiting http://short.est/GeAi9K should return  https://indicina.co..

The Solution was developed using .Net Core 6.0

The implementation is a RESTful API to perform the afore-mentioned task.

There several resources to:
1. Encode or shorted a Url
2. Decode or retrun the the full url
3. Retrieve a list of all encoded urls
4. Get statistics for an encoded url using the shortened alias
5. Retrun a full link for redirection using the alias or shorten link

Usage
1. Open the application with visual studio to pull dependencies
2. The API uses an in-memomry database, but it can also use MSSQL Server (in appSettings Un hash of insert the connection scrting to the MSSQL database in question and in StartUpExtentions.cs file change
	UseInMemoryDatabase to UseSqlServer and also the connection string)
3. Run the application, swagger is configured with the api for ease of testing


NOTE: A postman collection has also been added to the to project see link https://go.postman.co/workspace/My-Workspace~ead04592-78f4-4172-8ad7-7f39b97f18b3/collection/1408454-59c951ca-0757-4240-9c40-40ce44a797e8