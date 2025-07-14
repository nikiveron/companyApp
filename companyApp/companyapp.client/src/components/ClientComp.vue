<script setup>
  import { ref } from 'vue'
  const selectedTab = ref('agent')
</script>

<template>
  <div class="wrapper">
    <h3>В разработке!</h3>
  </div>
</template>

<style scoped>
  .wrapper {
    display: flex;
    place-items: flex-start;
    background: var(--color-background-mute);
    height: 50px;
    align-items:center;
  }

  h3 {
    color: #001798;
    font-weight: 500;
    margin: 0 0 0 2rem;
  }

</style>
