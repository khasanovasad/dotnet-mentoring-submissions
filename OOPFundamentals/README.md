# Coding Task
## Description

Given you are writing a file -cabinet software for a library. Currently the library works with the following document types: books, localized books, and patents and the following functionality is requested: 

1. Ability to search for document cards by a document number.  
2. The results should contain a list of card info which matches search requests. 
3. The card info varies based on the document type. 
4. Document cards are stored in the file system. Naming format is the following: type_#{number}.json 

Document attributes by types: 

* Patent (title, authors, date published, expiration date, unique id) 
* Book (ISBN, title, authors, number of pages, publisher, date published) 
* Localized book (ISBN, title, authors, number of pages, original publisher, country of localization, local publisher, date published)  

### Task 1:  
Create an application with the requested functionality (use the console app as UI for simplicity). Follow SOLID, DRY, KISS and YAGNI recommendations. Consider the ability to enhance the system in the following places: 

1. Storage format could be changed to database/cloud storage in the future 
2. New document types (like newspapers, magazines and so on) 
3. There could be multiple UIs in the future (Desktop, Web, Mobile) 

### Task 2: 
Add support for a new type of document – magazine (title, publisher, release number, publish date). Include it in the search and output parts. 

### Task 3: 

Add ability to cache card info once read. This should decrease the load on the storage system. Cache time should be configurable based on the document type. Some document types could have no expiration date (live long cache) for cache, and some should not be cached at all. 