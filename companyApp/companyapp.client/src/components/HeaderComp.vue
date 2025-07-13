<script setup>
  import { defineProps, defineEmits } from 'vue'

  const props = defineProps({
    selectedTab: String
   })
  const emit = defineEmits(['update:selected-tab'])

  function selectTab(tab) {
    emit('update:selected-tab', tab)
  }
</script>

<template>
  <nav class="tabs">
    <button :class="{ active: selectedTab === 'agent' }"
            @click="selectTab('agent')">
      Компании агентов
    </button>
    <button :class="{ active: selectedTab === 'bank' }"
            @click="selectTab('bank')">
      Банки партнеры
    </button>
    <button :class="{ active: selectedTab === 'client' }"
            @click="selectTab('client')">
      Компании клиентов
    </button>
  </nav>
</template>

<style>
  .tabs {
    display: flex;
    border-bottom: 1px solid #ccc;
    padding: 0;
  }

    .tabs button {
      background: none;
      border: none;
      outline: none;
      padding: 12px 24px;
      font-size: 16px;
      color: #888;
      cursor: pointer;
      border-bottom: 2px solid transparent;
      transition: color 0.2s, border-bottom 0.2s;
    }

      .tabs button.active {
        color: #222;
        border-bottom: 2px solid #222;
      }
</style>
