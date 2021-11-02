# Link Shortener

A URL shortening web application is where the user passes the full URL such as https://facebook.com/home in the giving field and it display a short URL such as http://short.est/GeAi9K for the user. There is another field where the user can input the shortened URL which will return
the long URL. Using the example above, visiting http://short.est/GeAi9K should return  https://facebook.com/home.

There is a session on the web application where you can see all the full url which as been shortened on the application by clicking on a GET ALL URL button.

The Web application was developed using Angular 11

# list of command

ng serve = To run the application

ng build = To build the application

ng build --prod = To build for production


# To change the baseUrl of the application

The baseUrl is said to be the url at which the application is using to communicate with the backend and it can be find inside the src folder in the application folder with the following path
src/environment and there are two different environment files, 1 for test and the other for production(environment.ts and environment.prod.ts)


