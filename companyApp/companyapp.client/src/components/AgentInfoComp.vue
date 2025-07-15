<script setup>
  import { ref, watch, onMounted } from 'vue'
  import { getAllAgents } from '../../api/agents'
  import axios from 'axios';

  const props = defineProps({
    filters: Object
  })

  const innValue = ref('')
  const phoneValue = ref('')
  const emailValue = ref('')
  const ogrnDateFromValue = ref('')
  const ogrnDateToValue = ref('')
  const priorityValue = ref(false)

  const emit = defineEmits(['apply-filters'])

  function applyFilters() {
    emit('apply-filters', {
      inn: innValue.value,
      phone: phoneValue.value,
      email: emailValue.value,
      ogrnDateFrom: ogrnDateFromValue.value,
      ogrnDateTo: ogrnDateToValue.value,
      priority: priorityValue.value
    })
    loadAgentsWithFilters({
      inn: innValue.value,
      phone: phoneValue.value,
      email: emailValue.value,
      ogrnDateFrom: ogrnDateFromValue.value,
      ogrnDateTo: ogrnDateToValue.value,
      priority: priorityValue.value
    }, 1)
  }

  const agents = ref([]);
  const pageNumber = ref(1);
  const pageSize = 10;
  const firstPage = ref('');
  const lastPage = ref('');
  const totalPages = ref(1);
  const totalRecords = ref(1);
  const nextPage = ref('');
  const previousPage = ref('');

  const API_URL = 'https://localhost:7166/agents'

  async function loadAgentsWithFilters(filters, page = 1) {
    try {
      console.log('Фильтры в таблице:', filters);
      const params = new URLSearchParams();
      params.append('PageNumber', page);
      params.append('PageSize', pageSize);
      if (filters.inn) params.append('Inn', filters.inn);
      if (filters.phone) params.append('PhoneNumber', filters.phone);
      if (filters.email) params.append('Email', filters.email);
      if (filters.ogrnDateFrom) params.append('OgrnFrom', filters.ogrnDateFrom);
      if (filters.ogrnDateTo) params.append('OgrnTo', filters.ogrnDateTo);
      if (filters.priority !== undefined) params.append('Priority', filters.priority);

      const url = `${API_URL}?${params.toString()}`;
      console.log('params:', params.toString());
      const response = await axios.get(url);
      agents.value = response.data.data;
      pageNumber.value = response.data.pageNumber;
      firstPage.value = response.data.firstPage;
      lastPage.value = response.data.lastPage;
      totalPages.value = response.data.totalPages;
      totalRecords.value = response.data.totalRecords;
      nextPage.value = response.data.nextPage;
      previousPage.value = response.data.previousPage;
    } catch (e) {
      console.error('Ошибка при получении агентов:', e)
    }
  }

  // Загружаем при первом монтировании
  onMounted(() => {
    loadAgentsWithFilters(props.filters || {}, 1)
  })

  // Следим за изменением фильтров
  watch(() => props.filters, (newFilters) => {
    loadAgentsWithFilters(newFilters || {}, 1)
  }, { deep: true })

  function currentFilters() {
    return {
      inn: innValue.value,
      phone: phoneValue.value,
      email: emailValue.value,
      ogrnDateFrom: ogrnDateFromValue.value,
      ogrnDateTo: ogrnDateToValue.value,
      priority: priorityValue.value
    }
  }

</script>

<template>
  <div class="filter-wrapper">
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
        <label for="ogrn-date-from">Дата присвоение ОГРН, от</label>
        <input type="date" id="ogrn-date-from" name="ogrn-date-from" class="input-field" v-model="ogrnDateFromValue" />
      </div>
      <div class="input-part">
        <label for="ogrn-date-to">Дата присвоение ОГРН, до</label>
        <input type="date" id="ogrn-date-to" name="ogrn-date-to" class="input-field" v-model="ogrnDateToValue" />
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
        <input type="checkbox" v-model="priorityValue" name="priority" id="check-priority" class="input-checkbox" />
        <label for="check-priority">Только приоритетные</label>
      </div>
    </div>
    <div class="input-block">
      <button type="button" class="filter-button" @click="applyFilters">
        Применить фильтрацию
      </button>
    </div>
  </div>
  <div class="data-wrapper">
    <div class="buttons-wrapper">
      <button class="button-wrap">Добавить +</button>
      <button class="button-wrap">Изменить </button>
      <button class="button-wrap">Удалить -</button>
    </div>
    <div class="table-wrapper">
      <table class="table-style">
        <thead>
          <tr>
            <th class="th-wrapper" style="width: 7%;">ID</th>
            <th class="th-wrapper" style="width: 25%;">ФИО представителя</th>
            <th class="th-wrapper" style="width: 25%;">Контактные данные</th>
            <th class="th-wrapper" style="width: 25%;">Компания</th>
            <th class="th-wrapper" style="width: 25%;">Дата присвоения ОГРН</th>
            <th class="th-wrapper" style="width: 25%;">Доступ к банкам партнерам</th>
            <th class="th-wrapper" style="width: 25%;">Приоритетный</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="agent in agents" :accesskey="agent.id">
            <td>{{ agent.agentId }}</td>
            <td>{{ agent.repLastName + " " + agent.repFirstName + " " + agent.repPatronymic }}</td>
            <td>{{ agent.repEmail + " " + agent.repPhone }}</td>
            <td>{{ agent.shortName }}</td>
            <td>{{ agent.ogrnDateOfIssue }}</td>
            <td>{{ agent.banks?.map(bank => bank.shortName).join(', ') }}</td>
            <td>{{ agent.priority ? "Да" : "Нет" }}</td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="pagination-wrapper">
      <button class="pagination-button-wrap"
              @click.prevent="loadAgentsWithFilters(currentFilters(), 1)"
              :class="{ disabled: pageNumber === 1 }">
        <<
      </button>
      <button class="pagination-button-wrap"
              @click.prevent="loadAgentsWithFilters(currentFilters(), pageNumber - 1)"
              :class="{ disabled: pageNumber === 1 }">
        <
      </button>
      <label class="page-number-label">{{ pageNumber }}</label>
      <button class="pagination-button-wrap"
              @click.prevent="loadAgentsWithFilters(currentFilters(), pageNumber + 1)"
              :class="{ disabled: pageNumber === totalPages }">
        >
      </button>
      <button class="pagination-button-wrap"
              @click.prevent="loadAgentsWithFilters(currentFilters(), totalPages)"
              :class="{ disabled: pageNumber === totalPages }">
        >>
      </button>
    </div>
  </div>
</template>

<style scoped>
  .filter-wrapper {
    display: flex;
    flex-direction: column;
    place-items: flex-start;
    color: black;
    background: #a6a6a6;
    padding: 0.5rem;
    width: 100%;
    min-width: 1000px;
    justify-content: space-between;
  }

  .filter-title {
    padding: 0;
    margin: 0;
    margin-bottom: 0.5rem;
  }

  .input-block {
    display: flex;
    place-items: flex-start;
    width: 100%;
    margin: 0.5rem 0 0 0;
    justify-content: flex-start;
  }

  .input-part {
    display: flex;
    margin: 0;
    padding: 0;
    width: max-content;
    flex-direction: column;
    flex-wrap: wrap;
  }

  label {
    margin: 0 0 0.25rem 0;
    font-weight: 400;
    font-size: 18px;
  }

  h3 {
    width: max-content;
  }

  .input-part3 {
    margin: 1.9rem 1rem 0 0;
    dispslay: flex;
    flex-direction: row;
    gap: 0.5rem;
    width: 300px;
  }

  input[type="checkbox"] {
    width: 1.2rem;
  }

  .input-field {
    padding: 8px 12px;
    border: 1px solid #ccc;
    border-radius: 10px;
    font-size: 14px;
    width: 400px;
    max-width: 90%;
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
  .data-wrapper {
    display: flex;
    flex-direction: column;
    width: 100%;
  }
  .buttons-wrapper{
    display: flex;
  }
  .button-wrap {
    margin: 1rem 0.5rem 1rem 0;
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

  .table-wrapper {
    color: var(--color-text-dark);
    background: var(--color-background-mute);
    text-align: center;
    display: inline-block;
    padding: 0.5rem 0.5rem;
    width: 100%;
  }
  .table-style {
    table-layout: fixed;
    width: 100%;
    word-wrap: break-word;
    border-collapse: collapse;
  }
    .table-style th {
      background: var(--color-background-soft);
      border: 1px solid var(--color-background-mute);
    }

    .table-style td {
      border: 1px solid var(--color-background-soft);
    }

.pagination-wrapper{
  margin: 0.5rem 0 0.5rem 0;
}

  .pagination-button-wrap {
    margin: 0 0.25rem 0 0.25rem;
    padding: 8px 15px;
    border: 1px solid var(--color-button);
    border-radius: 10px;
    width: 50px;
    color: white;
    background: var(--color-button);
    text-decoration: none;
    font: 500 14px arial;
    cursor: pointer;
    transition: background-color 0.2s;
  }

    .pagination-button-wrap:hover {
      background: var(--color-button-hover);
      border-color: var(--color-button-hover);
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
    }

    .pagination-button-wrap:last-child {
      margin-right: 0;
    }

.page-number-label{
  margin: 0 1rem 0 1rem;
  font:bold;
}

.disabled {
  pointer-events: none;
  opacity: 0.5;
}
</style>
