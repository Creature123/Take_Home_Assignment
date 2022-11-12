# SQL Test Assignment

Attached is a mysqldump of a database to be used during the test.

Below are the questions for this test. Please enter a full, complete, working SQL statement under each question. We do not want the answer to the question. We want the SQL command to derive the answer. We will copy/paste these commands to test the validity of the answer.

**Example:**

_Q. Select all users_

- Please return at least first_name and last_name

SELECT first_name, last_name FROM users;


------

**— Test Starts Here —**

#  for my own testing purpose user - 1=disabled; 2=enabled ( status );
# listing - 2=basic; 3=premium  ( status );

1. Select users whose id is either 3,2 or 4
- Please return at least: all user fields



**- Answer -**

select id,first_name,last_name,email,status,created from testest22.users where id in(2,3,4);


2. Count how many basic and premium listings each active user has
- Please return at least: first_name, last_name, basic, premium

**- Answer -**

WITH
TABLE1 AS ( SELECT u.id,u.first_name,u.last_name,count(l.status) basic FROM testest22.users u
inner join testest22.listings l
on l.user_id = u.id
where u.status = 2 and l.status =2
group by u.first_name,u.last_name,u.id),
TABLE2 AS (SELECT u.id,u.first_name,u.last_name,count(l.status) premium FROM testest22.users u
inner join testest22.listings l
on l.user_id = u.id
where u.status = 2 and l.status =3
group by u.first_name,u.last_name,u.id)
select TABLE1.FIRST_NAME,TABLE1.LAST_NAME, IFNULL(TABLE1.basic,0) BASIC,IFNULL(TABLE2.premium,0) premium from TABLE1 LEFT JOIN TABLE2 ON TABLE1.id = TABLE2.id;




3. Show the same count as before but only if they have at least ONE premium listing
- Please return at least: first_name, last_name, basic, premium


**- Answer -**

WITH
TABLE1 AS ( SELECT u.id,u.first_name,u.last_name,count(l.status) basic FROM testest22.users u
inner join testest22.listings l
on l.user_id = u.id
where u.status = 2 and l.status =2
group by u.first_name,u.last_name,u.id),
TABLE2 AS (SELECT u.id,u.first_name,u.last_name,count(l.status) premium FROM testest22.users u
inner join testest22.listings l
on l.user_id = u.id
where u.status = 2 and l.status =3
group by u.first_name,u.last_name,u.id)
select TABLE1.FIRST_NAME,TABLE1.LAST_NAME, IFNULL(TABLE1.basic,0) BASIC,IFNULL(TABLE2.premium,0) premium from TABLE1 LEFT JOIN TABLE2 ON TABLE1.id = TABLE2.id WHERE TABLE2.premium>0;


4. How much revenue has each active vendor made in 2013
- Please return at least: first_name, last_name, currency, revenue


**- Answer -**

select u.first_name , u.last_name , c.currency,SUM(c.price) revenue from testest22.users u inner join
testest22.listings l on u.id = l.user_id
inner join testest22.clicks c on l.id = c.listing_id
where u.status = 2 and YEAR(c.created) = 2013
group by u.first_name, u.last_name , c.currency;



5. Insert a new click for listing id 3, at $4.00
- Find out the id of this new click. Please return at least: id

**- Answer -**

a)
insert into testest22.clicks(listing_id,price,currency,created) values (3,4.00,'USD',now());

b)
select c.id from testest22.clicks c where
c.price = 4.00 and c.currency = 'USD' and c.listing_id = 3
and c.created =(select max(created) from testest22.clicks);




6. Show listings that have not received a click in 2013
- Please return at least: listing_name

**- Answer -**

select l.id,l.name from testest22.listings l where l.name not in
(select l.name from testest22.listings l inner join testest22.clicks c on
l.id = c.listing_id where YEAR(c.created) = 2013);



7. For each year show number of listings clicked and number of vendors who owned these listings
- Please return at least: date, total_listings_clicked, total_vendors_affected

**- Answer -**

<!-- I have got a confusion here whether I have to find out distinct listings and distinct user or
only distinct user and multiple listing id with different timeline

Please consider the Answer accordingly-->

<!-- Asssuming distinct user id and distinct Listing id e.g.
In the year 2013 : total distinct listing ID was 3 i.e
select distinct c.listing_id from testest22.clicks c where YEAR(c.created) = 2013 (6,2,10)
and the effected user ids are 1 and 2 since for listing 10 we do not have any user id
 -->

select distinct date,count(table1.total_listings_clicked) as total_listings_clicked,count(distinct table1.user_id) total_vendors_affected from
(select l1.year as date,id total_listings_clicked,l1.user_id fROM
(select distinct l.id as id,YEAR(c.created) as year,l.user_id from testest22.listings l
inner join testest22.clicks c on l.id = c.listing_id
group by l.id,c.created,l.user_id) as l1) as table1
group by date;




<!-- Assuming only distinct User id but for a year there can be multiple listings with multiple clicks

for example
In the year 2013 there are 10 listings but only effected users are 1,2
-->

select l1.year as date,SUM(l1.id) total_listings_clicked,count(distinct user) total_vendors_affected FROM
(select  count(l.id) as id,YEAR(c.created) as year,l.user_id as user from testest22.listings l
 inner join testest22.clicks c on l.id = c.listing_id
group by l.id,c.created,l.user_id) l1
group by year;




8. Return a comma separated string of listing names for all active vendors
- Please return at least: first_name, last_name, listing_names


**-- Answer --**

select u.first_name , u.last_name ,GROUP_CONCAT(l.name) listing_names from testest22.users u inner join testest22.listings l
on u.id = l.user_id
where u.status = 2
GROUP BY u.first_name,u.last_name;
