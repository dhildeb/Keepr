import { AppState } from '../AppState'
import { api } from './AxiosService'

class KeepsService {
  async getAll() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }

  async getById(id) {
    const res = await api.get('api/keeps/' + id)
    AppState.ActiveKeep = res.data
  }

  async create(data) {
    const res = await api.post('api/keeps', data)
    console.log(res.data)
    AppState.ActiveKeep = res.data
  }

  async update(id, data) {
    const res = await api.put('api/keeps/' + id, data)
    console.log(res.data)
    AppState.ActiveKeep = res.data
  }
}

export const keepsService = new KeepsService()
