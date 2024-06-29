import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import LoginView from "../views/LoginView.vue";
import BidView from "@/views/BidView.vue";
import RegisterView from "@/views/RegisterView.vue";
import AddBidView from "@/views/AddBidView.vue";
import BidByIdView from "@/views/BidByIdView.vue";

const routes = [
  {
    path: "/",
    name: "home",
    component: HomeView,
  },
  {
    path: "/bid",
    name: "bid",
    component: BidView,
  },
  {
    path: "/login",
    name: "login",
    component: LoginView,
  },
  {
    path: "/register",
    name: "register",
    component: RegisterView,
  },
  {
    path: "/addbid",
    name: "addbid",
    component: AddBidView,
  },
  {
    path: "/bidbyid/:id",
    name: "bidbyid",
    component: BidByIdView,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
