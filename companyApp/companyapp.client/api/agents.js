import axios from 'axios'
const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000/api'

export function getAllAgents() {
  return axios.get(`${API_URL}/agents/`)
}

export function getAgent(id) {
  return axios.get(`${API_URL}/agents/${id}`)
}

export function postAgent(data) {
  return axios.post(`${API_URL}/agents`, data)
}

export function putAgent(id, data) {
  return axios.put(`${API_URL}/agents/${id}`, data)
}

export function deleteAgent(id) {
  return axios.delete(`${API_URL}/agents/${id}`)
}
