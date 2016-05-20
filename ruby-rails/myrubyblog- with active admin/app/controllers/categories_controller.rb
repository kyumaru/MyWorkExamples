class CategoriesController < ApplicationController
  # GET /categories

  def index
    @categories = Category.all

   
  end

  # GET /categories/1

  def show
    @category = Category.find(params[:id])
	@posts=@category.posts
	@title=@category.name
  end

 end
