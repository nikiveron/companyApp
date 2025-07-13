<script setup>
  import { ref } from 'vue'
  const selectedTab = ref('agent')
  import Filter from './FilterComp.vue'
  import Info from './AgentInfoComp.vue'
</script>

<template>
  <div class="wrapper">
    <Filter/>
    <div class="divider"/>
    <Info/>
  </div>
</template>

<style scoped>
  .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }

  .divider {
    background: var(--color-background-soft);
    width: 2500px;
    height: 1rem;
  }
  /*
  .logo {
    display: block;
    margin: 0 auto 2rem;
  }

  @media (min-width: 1024px) {
    header {
      display: flex;
      place-items: center;
      padding-right: calc(var(--section-gap) / 2);
    }

    .logo {
      margin: 0 2rem 0 0;
    }

    header .wrapper {
      display: flex;
      place-items: flex-start;
      flex-wrap: wrap;
    }
  }
  */


</style>
