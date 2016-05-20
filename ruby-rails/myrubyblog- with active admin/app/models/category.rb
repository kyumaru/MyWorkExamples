class Category < ActiveRecord::Base
  attr_accessible :name
  has_many :posts	#the associations must be included in the table definition for the corresponding db migration file

end
