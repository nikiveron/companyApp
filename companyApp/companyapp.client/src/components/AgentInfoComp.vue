<script setup>
  import { ref, onMounted } from 'vue'
  import { getAllAgents } from '../../api/agents'

  const agents = ref([])
  onMounted(async () => {
    try {
      agents.value = await getAllAgents()
    } catch (e) {
      console.error('Ошибка при получении агентов:', e)
    }
  })

</script>

<template>
  <div class="wrapper">
    <div class="buttons-wrapper">
      <button class="button-wrap">Добавить +</button>
      <button class="button-wrap">Изменить </button>
      <button class="button-wrap">Удалить -</button>
    </div>
    <div class="view-wrapper">
      Таблица агентов
      {{agents}}
    </div>
  </div>
</template>

<style scoped>
  .wrapper {
    display: flex;
    place-items: flex-start;
    flex-direction:column;
  }
  .buttons-wrapper{
    display: flex;
    justify-content: flex-end;
    place-items: flex-end;
  }
  .button-wrap {
    margin: 0 0.5rem 0 0;
    padding: 8px 12px;
    border: 1px solid var(--color-button);
    border-radius: 10px;
    font-size: 14px;
    width: 100px;
    color: white;
    background: var(--color-button);
    cursor: pointer;
    transition: background-color 0.2s;
  }

    .button-wrap:hover {
      background: var(--color-button-hover);
      border-color: var(--color-button-hover);
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
    }
  
  .button-wrap:last-child {
    margin-right: 0;
  }

  .view-wrapper {
    color: var(--color-text-dark);
  }
</style>
