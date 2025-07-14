<script setup>
  import { ref } from 'vue'
  import Info from './AgentInfoComp.vue'

</script>

<template>
  <div class="wrapper">
    <Info/>
  </div>
</template>

<style scoped>
  .wrapper {
    display: inline-block;
    place-items: flex-start;
    flex-direction: column;
  }
</style>
