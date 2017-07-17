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

<u>Drawbacks of the tiers</u>
- performance costs
- increased complexity






