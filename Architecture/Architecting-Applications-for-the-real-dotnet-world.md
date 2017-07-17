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

#### 2.2.2 2<sup>nd</sup> 



