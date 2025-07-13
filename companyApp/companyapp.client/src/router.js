import { createRouter, createWebHistory } from 'vue-router'
//import AgentsPage from './components/AgentsPage.vue'

const routes = [
  {
    path: '/agents',
    name: 'Agents',
    //component: AgentsPage
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router 
