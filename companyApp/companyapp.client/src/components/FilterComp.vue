<script setup>
  import { ref } from 'vue'

  const innValue = ref('')
  const phoneValue = ref('')
  const emailValue = ref('')
</script>

<template>
  <div class="wrapper">
    <h2 class="filter-title">
      Настройка фильтра
    </h2>
    <div class="input-block">
      <div class="input-part">
        <h3>ИНН</h3>
        <input id="inn"
               type="text"
               placeholder="Введите ИНН"
               class="input-field"
               v-model="innValue" />
      </div>
      <div class="input-part">
        <h3>Дата присвоение ОГРН, от</h3>
        <input type="date" id="ogrn-date-from" name="ogrn-date-from" class="input-field" />
      </div>
      <div class="input-part">
        <h3>Дата присвоение ОГРН, до</h3>
        <input type="date" id="ogrn-date-to" name="ogrn-date-to" class="input-field" />
      </div>
    </div>
    <div class="input-block">
      <div class="input-part">
        <h3>Телефон</h3>
        <input id="phone"
               type="text"
               placeholder="Введите телефон"
               class="input-field"
               v-model="phoneValue" />
      </div>
      <div class="input-part">
        <h3>Email</h3>
        <input id="email"
               type="text"
               placeholder="Введите Email"
               class="input-field"
               v-model="emailValue" />
      </div>
      <div class="input-part3">
        <input type="checkbox" checked name="priority" class="input-checkbox"/> 
        <h3>Только приоритетные</h3>
      </div>
    </div>
    <div class="input-block">
      <button type="submit" class="filter-button">
        Применить фильтрацию
      </button>
    </div>
  </div>
</template>

<style scoped>
  .wrapper {
    display: flex;
    flex-direction: column;
    place-items: flex-start;
    color: black;
    background: #a6a6a6;
    padding: 0.5rem;
  }

  .filter-title {
    padding: 0;
    margin: 0;
    margin-bottom: 0.5rem;
  }

  .input-block {
    display: flex;
    place-items: flex-start;
    margin: 0.5rem 0 0 0;
  }

  .input-part {
    margin: 0 1rem 0 0;
  }
  .input-part3 {
    margin: 1.9rem 1rem 0 0;
    display: flex;
    flex-direction: row;
    gap: 0.5rem;
  }

  input[type="checkbox"] {
    width: 1.2rem;
  }

  .input-field {
    padding: 8px 12px;
    border: 1px solid #ccc;
    border-radius: 10px;
    font-size: 14px;
    width: 300px;
  }

    .input-field:focus {
      outline: none;
      border-color: #007bff;
      box-shadow: 0 0 0 2px rgba(0, 123, 255, 0.25);
    }

  input[type="date"] {
    font-weight: 500;
    font-family: inherit;
    font-size: 14px;
    color: var(--vt-c-text-light-2);
  }

    input[type="date"]::-webkit-calendar-picker-indicator {
      cursor: pointer;
    }

    input[type="date"]::-webkit-datetime-edit {
      font-family: inherit;
      font-size: 14px;
    }

    input[type="date"]::-webkit-datetime-edit-fields-wrapper {
      font-family: inherit;
      font-size: 14px;
    }

    input[type="date"]::-webkit-datetime-edit-text {
      font-family: inherit;
      font-size: 14px;
    }

    input[type="date"]::-webkit-datetime-edit-month-field,
    input[type="date"]::-webkit-datetime-edit-day-field,
    input[type="date"]::-webkit-datetime-edit-year-field {
      font-family: inherit;
      font-size: 14px;
    }

  .filter-button {
    margin: 0.5rem 0 0 0;
    padding: 8px 12px;
    border: 1px solid var(--color-button);
    border-radius: 10px;
    font-size: 14px;
    width: 200px;
    color: white;
    background: var(--color-button);
    cursor: pointer;
    transition: background-color 0.2s, border-color 0.2s;
  }
    .filter-button:hover {
      background: var(--color-button-hover);
      border-color: var(--color-button-hover);
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
    }
</style>
