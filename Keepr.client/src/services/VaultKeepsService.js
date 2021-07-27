import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { api } from './AxiosService'

class VaultKeepsService {
  async getKeepsByVaultId(id) {
    try {
      const res = await api.get(`api/vaults/${id}/keeps`)
      AppState.keeps = res.data
    } catch (error) {
      Pop.toast(error)
    }
  }

  async addToVault(vaultId, keepId) {
    const data = { vaultId, keepId }
    await api.post('api/vaultkeeps', data)
    Pop.toast('Keep sucessfully added!', 'success', 'top-end', 1000)
  }

  async delete(id) {
    const res = await api.delete('api/vaultkeeps/' + id)
    const i = AppState.keeps.findIndex(k => k.id === res.data.keepId)
    AppState.keeps.splice(i, 1)
    Pop.toast(res.data)
  }
}
export const vaultKeepsService = new VaultKeepsService()
