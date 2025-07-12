<template>
  <div class="agents-page">
    <div v-if="loading">Загрузка...</div>
    <div v-else-if="error" class="error">{{ error }}</div>
    <pre v-else class="raw-json">{{ jsonData }}</pre>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const jsonData = ref('')
const loading = ref(true)
const error = ref('')

const fetchAgents = async () => {
  try {
    const response = await fetch('https://localhost:7166/agents', {
      method: 'GET',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      }
    })
    
    
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status} - ${response.statusText}`)
    }
    
    const data = await response.json()
    jsonData.value = JSON.stringify(data, null, 2)
  } catch (err) {
    error.value = `Ошибка загрузки: ${err.message}. Убедитесь, что сервер запущен на https://localhost:7166`
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  fetchAgents()
})
</script>

<style scoped>
.agents-page {
  padding: 20px;
  font-family: monospace;
  white-space: pre-wrap;
  word-wrap: break-word;
}

.raw-json {
  margin: 0;
  font-size: 14px;
  line-height: 1.4;
}

.error {
  color: red;
  font-weight: bold;
}
</style> 
