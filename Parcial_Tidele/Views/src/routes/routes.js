import DashboardLayout from "@/pages/Layout/DashboardLayout.vue";

import Index from "@/pages/Index.vue";

const routes = [
  {
    path: "/",
    component: DashboardLayout,
    redirect: "/main",
    children: [
      {
        path: "main",
        name: "Menu Principal",
        component: Index
      }
    ]
  }
];

export default routes;
