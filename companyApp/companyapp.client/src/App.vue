<script setup>
  import { ref } from 'vue'
  import HeaderComp from './components/HeaderComp.vue'
  import AgentComp from './components/AgentComp.vue'
  import BankComp from './components/BankComp.vue'
  import ClientComp from './components/ClientComp.vue'
  const selectedTab = ref('agent')
</script>

<template>
  <div id="app">
    <header>
      <HeaderComp :selected-tab="selectedTab"
                  @update:selected-tab="selectedTab = $event" />
    </header>

    <div v-if="selectedTab==='agent'">
      <AgentComp />
    </div>
    <div v-else-if="selectedTab==='bank'">
      <BankComp />
    </div>
    <div v-else-if="selectedTab==='client'">
      <ClientComp />
    </div>

  </div>
</template>

<style scoped>
    header {
      display: flex;
      place-items: flex-start;
    }
</style>
