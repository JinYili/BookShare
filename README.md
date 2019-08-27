# BookShare


HOW TO RUN
The easiest way to run is install the vs2017 then run from it. 
When the application is start, then go to https://localhost:5001/

WHAT HAS INCLUDED

  1. CRUD function for the book 
  2. user and role management (only for user role admin, some functions are from framework but I has extended) 

ABOUT API

the API is made in controller book.cs
For example:

[GET] https://localhost:5001/api/v1/book/All
Return the book in json foramt

[GET] https://localhost:5001/api/v1/book/AllInfo
Return the book and comment in json format

[GET] https://localhost:5001/api/v1/book/View/:id
Return single book and its comment which according by the given id

[GET] https://localhost:5001/api/v1/book/AllComment
Return all the comments

[GET] https://localhost:5001/api/v1/book/ViewComment/:id
Return all the comment for single book by given book id

[POST] https://localhost:5001/api/v1/book/add
Create a new book, request body like
    {
        "title": "Fan hua",
        "author": "Jin Chenyu",
        "releaseDate": "2013-05-09T00:00:00",
        "price": 11.00,
        "coverUrl": "https://img2-placeit-net.s3-accelerate.amazonaws.com/uploads/stage/stage_image/37837/large_thumb_stage.jpg",
    }
	
[POST] https://localhost:5001/api/v1/book/AddComment
Add a comment to a book, request body like
	{
		bookId: 15,
		content : "hahah"
	}

[PUT]https://localhost:5001/api/v1/book/Edit/:id
Edit book by given id ,request body like
    {
        "id": 17,
        "title": "The Three-Body Problem",
        "author": "Ken Liu",
        "releaseDate": "2013-04-06T00:00:00",
        "price": 108.00,
        "coverUrl": "https://upload.wikimedia.org/wikipedia/en/thumb/0/0f/Threebody.jpg/220px-Threebody.jpg",
        "comments": []
    }
	
[Delete]https://localhost:5001/api/v1/book/delete/:id
Delete book by given id

TEST
There is a unitTest project included in solution, it has four test cases. 

ATTENTION: HEADER SHOULD HAS "Content-Type	application/json; charset=utf-8"