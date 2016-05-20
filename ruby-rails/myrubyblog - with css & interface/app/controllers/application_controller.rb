class ApplicationController < ActionController::Base
  protect_from_forgery
  
  helper_method :all_categories
  helper_method :all_posts	
 
  def all_categories
	@categories = Category.all #global
  end 
  
  def all_posts
	@posts=Post.all
  end
  
  
end
