import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
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
    AppState.keeps = [res.data, ...AppState.keeps]
  }

  async update(id, data) {
    const res = await api.put('api/keeps/' + id, data)
    console.log(res.data)
    AppState.ActiveKeep = res.data
  }

  async delete(id) {
    try {
      const res = await api.delete('api/keeps/' + id)
      Pop.toast(res.data)
      AppState.keeps = AppState.keeps.filter(k => k.id !== id)
    } catch (error) {
      Pop.toast(error)
    }
  }
}

export const keepsService = new KeepsService()
