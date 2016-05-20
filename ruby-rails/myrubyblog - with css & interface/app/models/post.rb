#model is the layer of the system responsible for representing business data and logic
#form validation goes in the model before db operations
#a model should have a singular name, rails g model category name:string 
#the above command creates the this file and a data migration, look /db
#the data migration file contains the definition of the table in ruby code
#the migration can be used to generate sql code onto a db using, rake db:migrate
#creating the actual db tables. Some fields like id and timestamp are added implicitly
#to every table generated in such way. 
#http://stackoverflow.com/questions/15279101/when-you-create-a-model-using-rails-generate-model-or-rails-scaffold-does-it-gen
#http://edgeguides.rubyonrails.org/active_record_migrations.html
#ActiveRecord is an object that pulls its data from a db and defines operations over them
#rails works with ActiveRecords not with db result sets, since an AR is an object it is already encapsulated
#http://guides.rubyonrails.org/active_record_basics.html#what-is-active-record-questionmark
#for referential integrity and relations, like foreign keys, use associations
#http://guides.rubyonrails.org/association_basics.html#why-associations-questionmark
# to add a new active record hashes are usually used along with create new
#http://guides.rubyonrails.org/active_record_basics.html#create
#insert new AR from console, rails c, to view all AR, User.all # Where user is your model.
#query AR with id=1,  post=Post.find 1, query for the body field, post.body
#update body, post.body='updated text', commit changes, post.save, or use Update
#validation sticks inside of the model
class Post < ActiveRecord::Base
  attr_accessible :author_id, :body, :category_id, :title
  
  belongs_to :category #one post belongs to one category only, the associations must be included in the table definition for the corresponding db migration file

#validates is a helper method that receives a number of fields to check
#condition rules for, each call to validates will generate errors if a
#condition is not met, any view with a form associated to such model
#wont submit if there is an error. In order to show such errors, the view
#has got to have the code to do so. 

  validates :title,:body,:presence=>true #title and body fields cannot be blank 
  validates :title,:length=>{:minimum=>2} #title has a minimum length of 2 chars
 #validates :title,:uniqueness=>true #enforces unique constraint over title, default error message
  validates :title,:uniqueness=>{:message=>"already taken, must be unique"} #enforces unique constraint over title, default error message

end
