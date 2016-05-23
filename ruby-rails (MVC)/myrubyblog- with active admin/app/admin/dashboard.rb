# gem 'activeadmin', "0.6.0" #only this version supports dashboard editing
#code here to customize the dashboard
#http://railscasts.com/episodes/284-active-admin
#section, column, table_for are all methods
#priority is being used to show posts first to the left
ActiveAdmin::Dashboards.build do
	section "recent posts", priority: 1 do
		table_for Post.order("id desc").limit(15) do 
		   column :id
		   column "post title", :title do |post|
		      link_to post.title, [:admin, post]
		   end
		    column :category
			column :created_at
		end
     strong { link_to "View All Posts", admin_posts_path }
	end

	section "recent categories" do
		table_for Category.order("id desc").limit(15) do 
		   column :id
		   column "post name", :title do |category|
		      link_to category.name, [:admin, category]
		   end
			column :created_at
		end
     strong { link_to "View All Categories", admin_categories_path }
	end
	
end
