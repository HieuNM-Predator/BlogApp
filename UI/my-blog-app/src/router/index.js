import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import CategoryView from "@/views/CategoryView.vue";
import BlogPostView from "@/views/BlogPostView.vue";
import AddBlogPostView from "@/views/AddBlogPostView.vue";
import EditBlogPostView from "@/views/EditBlogPostView.vue";
import BlogDetailsView from "@/views/BlogDetailsView.vue";

const routes = [
  {
    path: "/",
    name: "home",
    component: HomeView,
  },
  {
    path: "/about",
    name: "about",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/AboutView.vue"),
  },
  {
    path: "/admin/categories",
    name: "admin-categories",
    component: CategoryView,
  },
  {
    path: "/admin/blogposts",
    name: "admin-blogposts",
    component: BlogPostView,
  },
  {
    path: "/admin/add-blogpost",
    name: "admin-addBlogPost",
    component: AddBlogPostView,
  },
  {
    path: "/admin/blogpost/:blogPostId",
    name: "admin-editBlogPost",
    component: EditBlogPostView,
  },
  {
    path: "/blogpost/:url",
    name: "blogPost",
    component: BlogDetailsView,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
