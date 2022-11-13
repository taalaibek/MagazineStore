# Vertmarkets Programming Challenge
# Welcome to the Magazine Store
Our magazine store exposes a series of REST endpoints around the magazine subscription
business.

We have magazines spread out across several categories. We also expose an endpoint to retrieve
subscribers with information about which magazines they are currently receiving.

Your challenge involves the following: You must write a c# console application that utilizes the API
endpoints to identify all subscribers that are subscribed to at least one magazine in each
category. You must then submit their id's to the answer endpoint and print the result to the
console.

The answer endpoint will return a result object, that will let you know how long it took for you to
get to the answer as well as if it was correct.
Each run of the application must start with retrieving a token from the token endpoint, which
you will have to pass along to any subsequent call. Don't reuse the token across multiple runs of
your program as they expire.

# Submission
Submit your code as well as a README file with instructions on how to run your program. You may
either submit a zip file or a link to a public source control repository to dev@vertmarkets.com

# Extra Credit
Once you run through your solution a couple of times, you will notice that sometimes it runs faster
and sometimes slower. This is because some of the operations are throttled to take anywhere from
1 to 4 seconds.

For extra credit, implement a solution that will improve the performance of your application. You
should be able to consistently get it to run in under 10 seconds.

# API Information
There are 5 endpoints exposed at this base URL: http://magazinestore.azurewebsites.net. You can
get detailed documentation about our API here: http://magazinestore.azurewebsites.net/api-docs.
- /api/token - This will return a token that must be used for all. subsequent calls to the API.
- /api/categories/{token} - This endpoint will return our current magazine categories
- /api/magazines/{token}/{category} - This endpoint will return magazines for the specified category
- /api/subscribers/{token} - This endpoint will return our current subscribers along with the magazine ids that they are subscribed to.
- /api/answer/{token} - You will have to POST your answer to this endpoint.
