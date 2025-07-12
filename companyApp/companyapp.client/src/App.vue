<script setup>
  import { ref } from 'vue'
  import HeaderComp from './components/Header.vue'
  const selectedTab = ref('agent')
</script>

<template>
  <div id="app">
    <!--<router-view />-->
    <header>
      <HeaderComp
      :selected-tab="selectedTab"
      @update:selected-tab="selectedTab = $event"
    />
    </header>
    <span>Выбрано: {{selectedTab}}</span>
  </div>
</template>

<style scoped>
    header {
      display: flex;
      place-items: flex-start;
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
