# a controller defines actions=methods,
# there are 7 in rails new, index, edit, create, destroy...
# once an action is defined, to display its stuff, a view should be created for it
# then such a controller should be made available by configuring it as a 
# resource route in routes.rb, so its actions can be accessed by URL 
# update, create and delete actions, do work for another actions so usually
# dont have a view, to generate a view
# rails g <controller name> <view name1, name2...> --skip, skips created files
# when generating views after controller creation but some extra routes are added
# to config.rb and was causing a routing error.  
# controller names are plural, model names must be singular
# rake routes, to view mapping of all actions by URL
# a controller transfers data between the model(db) and the views by its actions
class PostsController < ApplicationController #every controller inherits from ApplicationController which is the initial controller provided by framework
  
  def index	#the / when requesting this controller http://localhost:3000/posts
	@myvar =Time.now #@anything is an instance variable, data from model is stored and then passed to view
  #lets create a var that holds all posts
	@posts=Post.all
	@categories = Category.all #to make an instance variable global a helper method for the application controller class must be written
  end
  
  #used to show new content,
  def show
	@post=Post.find(params[:id])# URL is used to get the post id to find
  end
  
  #edit renders a view with a form in it and submits to update action
  #i.e when clicking a submit button
  def edit 
	@post=Post.find(params[:id])# URL is used to get de post id to find, @post gets assigned a post object, if a form is created for it, it will map such object properties 
  end
 
 #update does not render a view, gets a submitted request plus data to update 
 #db from the action edit 
  def update
	@post=Post.find(params[:id])#params uses the URL to get de post id object
	if @post.update_attributes(params[:post])# gets passed in a post from URL
		redirect_to posts_path, :notice=>"the post was updated" #posts_path is a named route, can be inferred from command rake routes result
	else
		render "edit"
	end
  end
#new action renders a view and submits data to create action to get the job done 
  def new #http://localhost:3000/posts/new
	@post = Post.new #creates a new empty post,if a form is created for it, it will be empty as well  
  end
#create gets submitted a job request along with data, and does the db insert of new data
  def create
	@post=Post.new(params[:post])#params uses the URL to get the post object 	
	if @post.save #yes the method post returns boolean
		redirect_to posts_path, :notice=>"a new Post was saved" #this is a flash message, it must be enabled in the layout file of the project, \app\views\layouts\application.html.erb 
	else
		render "new" #renders a view
	end
  end
  
  def destroy
	@post=Post.find(params[:id])#params uses the URL to get de post id object
	@post.destroy
	redirect_to posts_path, :notice=>"Post deleted" #this is a flash message, it must be enabled in the layout file of the project, \app\views\layouts\application.html.erb 
  end
  
end
