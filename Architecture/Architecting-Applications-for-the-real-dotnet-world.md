# Architecting applications for the real .net world

## 1. Real world architectural thinking

### 1.1 Lean software development

#### 1.1.1 Lean principles

* Eliminate waste
* Amplify learning
* Decide as late as possible
* Deliver as fast as possible
* Empower the team
* Build integrity in (Automated testing)
* See the whole

#### 1.1.2 Miminum Valuable Product (MVP)

##### 1.2.2.1 MVP goals

The goal : validated learning 
* What was the response ?
* What did they like ? Hate ?
* How much profit ?
* Is it worth scaling ?
* Bad assumption ?

##### 1.2.2.2 MVP : What we don’t care about

* scalability
* maintenance cost
* and often:
  * beauty
  * code cleanliness
  * full feature set
  * performance
  * security
  
  
### 1.2 Agile architecture

Date(fast) [Missed deadline] / cost (cheap) [Blown budget] / Quality (good) [Technical debt]

### 1.3 Layers

#### 1.3.1 The layers

| Layer        | Technology                                |       |
|:-------------|:------------------------------------------|:------|
| Presentation	|	WebForms, MVC, WPF, Winforms, Silverlight	| _UI_  |
| Service		    |	Web API, WCF, ServiceStack, POCOs    			  | _API_ |
| Domain       |	C#										                              | _BLL_ |
| Data									| ADO.NET, ORM, Stored procedure            |	_DAL_ |


#### 1.3.2 Layers vs Tiers

* Layers are _logical_
* Tiers are _physical_

##### 1.3.2.1 Tiers

- scalability :  we could scale our data access layer separately from our presentation layer
- security : we could add firewall between each other of your physical tiers
- uptime : multiply the server to avoid the downtime during the maintenance
- reusable : the tiers dedicated to the database could be use by different application hosted on different tiers

__Drawbacks of the tiers__  
- performance costs
- increased complexity

##### 1.3.2.2 Layers

- separate concerns
- aid understanding
- abstract away complexity
- support testing
- minimize dependences
- enable reuse

__Drawbacks__
- leak (mix logical layers together)
- more code

## 2. Business Logic Layer

### 2.1 BBL

- center of the app
- handles logic
- manages behaviors

### 2.2 Business Layer patterns (.net)

There are 4 patterns:
- Transaction script (complexity —)
- table module 
- active record
- domain model (complexity ++)

And we can classify the pattern:
* _Procedural_ : Transaction script
* _Data-driven_ : table module / Active Record (the DB structure dictates the structure of the classes => very data centric, you can use it only for the data centric application
* _Business-driven_ : Domain Design Driven

#### 2.2.1 1<sup>st</sup> BLL approach : Transaction script

- simple
- procedural
- one public function per UI operation


| UI                     | Method                    |
|:-----------------------|:--------------------------|
| button RegisterSpeaker | public RegisterSpeaker()  |
| button SubmitOrder			  | public SubmitOrder()      |

=> handles everything 	
 
__drawbacks__
- risk of duplication
- break the Single Responsibility Principle 
- becomes painful as logic complexity grows

#### 2.2.2 2<sup>nd</sup> BLL approach : Table module

- Each class represents a table : the class is the abstraction over the a database table
- the class implements DataTable, DataSet
- the table module approach is gone out of the vogue as Entity Framework has become the most popular 	option for the DAL

__Drawbacks__
- considered a legacy approach
- this approach does tightly bind your business logic together with your data access layer.
- the structure of your DataTable will traditionally match your database schema.

#### 2.2.3 3<sup>rd</sup> BLL approach : Active Record

- each instance represents a DB row
- class knows how to persist itself
- contain business logic
- can add static methods to work on all table records
- often implemented using : Entity Framework / Linq-to-SQL / Subsonic / Castle ActiveRecord (abstraction over NHibernate)

__Pros__
- Simple and obvious
- Speedy dev and changes
- compliments CRUD app
- good for simple domain
- No OR mismatch
- Honors YAGNI

__Drawbacks__
- Rigid : Domain model = DB => if the database model changes, you have to update the business logic layer
- leads to god object (anti-pattern)
- low cohesion : break Single Responsibility Principle : by definition, this pattern mixes the data access with the business logic
- hard to test because the the BBL is highly tied to the database. It’s very difficult to inject a DB mock
- tricky transaction: each instance knows how to save itself, so it can be difficult to manage the transaction


#### 4<sup>th</sup> BLL approach : DDD

What if I designed my classes without worrying what the database looks like ?  
How would I design my object model to best solve the business problem ?  

The big idea : structure your business logic however you desire without feeling constrained about the database structure.	

For instance:
_DB Tables_ : Customer / Address / Purchase  
_Classes_ : Member (with list of addresses) / Order  

__Pros__
- Manage complexity
- Leverage design patterns
- Speak business language
- Abstract ugly DB schema
- Compliments large team
- Reusable
 
__Cons__
- learning curve
- time-consuming design
- long-term commitment
- DB mapping overhead

## 3. Service Layer

### 3.1 Goal

Service layer helps coordinate interactions among your domain objects and provides ac coarse grained API (the consumer does not need to understand the detail behind)  

### 3.2 Service Layer in a nutshell

- usually sits between the presentation and the business logic
- typically called by presentation	
- really a facade pattern (encapsulate a complicated object to wrap it up in something easier to understand)
- takes requests from one layer and sends to another
- conceptually similar to transaction script
- can centralize handling cross-cutting edge

Service Layer should be thin

### 3.3 Roles 

Core role:
- adapt data into format the representation layer requires
- delegate work to business objects

Potential roles
- security (authorization, managing user roles)
- logging 
- searching
- notification
- binding
- managing transaction
- data validation

The service layer is __like a boss__
- does not perform any task directly
- orchestrates interactions between business object
- just like boss who organizes work between people. Accomplish task through others.

The service layer is __a boundary__
- separates the presentation layer from the business layer

The service layer is __a shield__
- shields the presentation layer from business logic complexity
- supplies generic and common interface
- keeps layer loosely coupled


### 3.4 Fine vs grain coarse API

- course grained : Facade pattern
- Manages interactions among domain layer objects
- Encapsulate business logic - abstract away details
- sharable - optionally a web service  

### 3.5 When ? Where ?

When to use a service layer ?
- Multiple UIs : centralize all your logic 
- Multiple consumer of business logic
- Complex interactions among domain objects

Where is it called ?
- Services are traditionally invoked from the perception layer 
	— WebForm : the code behind
	— MVC : controller

### 3.6 Implementation and technologies

To implement the service layer, we could use different technologies:
- WCF
- WebAPI
- ServiceStack
_ POCOs (Plain Old CLR Objects) via a shared library


### 3.7 Web Service vs Shared Library

#### 3.7.1 Web Service pros

- Immediate bug fixes for all clients
- Clients can easily upgrade when desired (when versioned API offered) : we can support multiple versions of the web service simultaneously.
-  consumers can not decompile the code
- Autonomous : can scale service hardware separately (you can have a tier with just a web service )
- Tech agostic : a java client can consume a .net web service if the endpoint returns a JSON or XML response.
- enforce the operation of concerns : UI cannot “route around”
- highly scalable
- centralised deployments

#### 3.7.2 Shared library pros

- Native code called in the process : higher performance
- no serialisation overhead
- No internet connection required
- No risk of centralized service going down and impacting all consumers
- No risk of public abuse (no people gaining unauthorized access)
- Simplest thing that could possibly work : thus default to this.
    
### 3.8 What should the service layer return ?

There are 3 options:
- __Data Transfert Object__ (DTO) :  a class with no method inside
- __Copy of domain entities without behaviour__ :  no calling method with your domain 
- __Domain entity__ :  you expose your domain entity out the service layer

### 3.9 DTO

#### 3.9.1 What is a DTO ?

“Object that carries data across an application’s boundaries with the primary goal of minimising round trips”. Martin Fowler

A class with data only, no methods.  
Avoid coupling between UI and domain layers.

#### 3.9.2 When using DTO ?

1. circular reference : DTO can solve the problem when you want to save the data. For instance N user <-> N adresses : you cannot save those objects.  
2. Domain is not on same physical tier as service  
3. Domain object would mean bloated response : a DTO can provide only the properties needed by the UI or the data are too complex to be binding to UI  

### 3.10 Summary

- service layer is an intermediary
- not all applications need a service layer
- DTOs are useful but optional

