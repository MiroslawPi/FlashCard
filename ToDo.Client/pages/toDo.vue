<template>
  <div>
    <v-card outlined>
      <v-card-title>
        ToDo - Task List <!--  {{ taskListId }} -->
      </v-card-title>
    </v-card>
    <v-card v-if="showTable" outlined>
      <v-card-title>
        <v-btn
          color="primary"
          @click="editItem('00000000-0000-0000-0000-000000000000')"
        >
          Dodaj listę
        </v-btn>
        <v-spacer /> Tabela listy zadań ToDo
        <v-spacer />
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Szukaj"
          class="mx-4"
          clearable
        />
      </v-card-title>
      <v-data-table
        dense
        :headers="headers"
        :items="items"
        :expanded.sync="expanded"
        :options.sync="options"
        show-expand
        :search="search"
      >
        <template #[`item.id`]="{ item }">
          <v-btn
            small
            color="primary"
            @click.stop="editTask('00000000-0000-0000-0000-000000000000', item.id)"
          >
            <v-icon small>
              mdi-calendar-check
            </v-icon>
            Dodaj taska
          </v-btn>
          <v-btn
            small
            color="primary"
            @click.stop="editItem(item.id)"
          >
            <v-icon small>
              mdi-file-document-edit-outline
            </v-icon>
            Edycja
          </v-btn>
          <v-btn color="error" small class="mx-1" @click="deleteList(item.id)">
            <v-icon small>
              mdi-delete-forever
            </v-icon>
            Usuń !
          </v-btn>
        </template>
        <template #expanded-item="{ headers, item }">
          <td :colspan="headers.length">
            <list-of-tasks-detail :item="item" @editTask="editTask" @updateTask="updateTask" @deleteTask="deleteTask"/>
          </td>
        </template>
      </v-data-table>
      <v-dialog v-model="showEditor" max-width="600">
        <list-of-tasks-editor
          :id="itemId"
          :key="itemId"
          @cancel="cancelItem"
          @save="saveList"
          @deleteList="deleteList"
        />
      </v-dialog>
      <v-dialog v-model="showTaskEditor" max-width="600">
        <task-editor
          :id="taskId"
          :key="taskId"
          @cancel="cancelTask"
          @save="saveTask"
          @deleteTask="deleteTask"
        />
      </v-dialog>
    </v-card>
  </div>
</template>

<script>
import ListOfTasksDetail from '~/components/ListOfTasksDetail.vue'
export default {
  components: { ListOfTasksDetail },
  data: () => ({
    headers: [
      { value: 'name', text: 'Nazwa', sortable: true },
      { value: 'description', text: 'Opis', sortable: true },
      { value: 'czasGenModForRead', text: 'Utworzono', sortable: true },
      { value: 'id', text: 'Operacje', sortable: false },
      { text: 'Taski', value: 'data-table-expand' }
    ],
    items: [],
    name: '',
    description: '',
    task: { listId: null, taskName: null, taskDescription: null },
    options: {},
    listOfTasks: {},
    showEditor: false,
    showTaskEditor: false,
    expanded: [],
    search: '',
    componentKey: 0,
    componentKeyTask: 0,
    confirmdel: false,
    showTable: true,
    itemId: null,
    taskId: null,
    taskListId: null
  }),
  async fetch () {
    await this.getGenerated()
  },
  methods: {
    async getGenerated () {
      await this.$axios.get('/api/ListOfTasks')
        .then((response) => {
          this.items = response.data

          this.refactorColumn()
        })
        .catch((error) => {
          console.log(error)
          this.errorMsg = 'Error geting data'
        })
    },
    refactorColumn () {
      for (let i = 0; i < this.items.length; i++) {
        this.items[i].czasGenModForRead = new Date(this.items[i].created).toISOString().slice(0, 10) + ' ' + new Date(this.items[i].created).getHours() + ':' + new Date(this.items[i].created).getMinutes()
      }
    },
    editItem (id) {
      this.itemId = id
      this.showEditor = true
    },
    editTask (id, taskListId) {
      this.taskId = id
      this.taskListId = taskListId
      this.showTaskEditor = true
    },
    cancelItem () {
      this.itemId = null
      this.taskListId = null
      this.showEditor = false
    },
    cancelTask () {
      this.taskId = null
      this.showTaskEditor = false
    },
    async saveList (item) {
      if (item.id === '00000000-0000-0000-0000-000000000000') {
        try {
          await this.$axios.$post('/api/ListOfTasks/', JSON.stringify({ name: item.name, description: item.description }), {
            headers: {
              'Content-Type': 'application/json'
            }
          })
        } catch (error) {
          this.errorMsg = 'Error geting data'
        }
      } else {
        try {
          await this.$axios.$put('/api/ListOfTasks/', JSON.stringify({ id: item.id, name: item.name, description: item.description }), {
            headers: {
              'Content-Type': 'application/json'
            }
          })
        } catch (error) {
          this.errorMsg = 'Error putting data'
        }
      }

      this.showEditor = false
      this.itemId = null
      this.$fetch()
    },
    async saveTask (item) {
      if (item.id === '00000000-0000-0000-0000-000000000000') {
        try {
          await this.$axios.$post('/api/TaskFromList/', JSON.stringify({ id: item.id, listId: this.taskListId, name: item.name, description: item.description }), {
            headers: {
              'Content-Type': 'application/json'
            }
          })
        } catch (error) {
          this.errorMsg = 'Error geting data'
        }
      } else {
        try {
          await this.$axios.$put('/api/TaskFromList/', JSON.stringify({ id: item.id, name: item.name, description: item.description }), {
            headers: {
              'Content-Type': 'application/json'
            }
          })
        } catch (error) {
          this.errorMsg = 'Error putting data'
        }
      }

      this.showTaskEditor = false
      this.taskId = null
      this.$fetch()
    },
    async deleteList (id) {
      try {
        await this.$axios.$delete(`/api/ListOfTasks/${id}`)
      } catch (error) {
        this.errorMsg = 'Error putting data'
      }
      this.showEditor = false
      this.$fetch()
    },
    async deleteTask (id, listId) {
      try {
        await this.$axios.$delete('/api/TaskFromList/',
          {
            headers: {
              'Content-Type': 'application/json'
            },
            data: {
              id,
              listId
            }
          }
        )
      } catch (error) {
        this.errorMsg = 'Error putting data'
      }
      this.$fetch()
    },
    async updateTask (id, name, description, completed) {
      try {
        await this.$axios.$put('/api/TaskFromList/', JSON.stringify({ id, name, description, completed }), {
          headers: {
            'Content-Type': 'application/json'
          }
        })
      } catch (error) {
        this.errorMsg = 'Error geting data'
      }
    },
    async addTask () {
      try {
        await this.$axios.$post('/api/ListOfTasks/task', JSON.stringify({ listId: this.task.listId, taskName: this.task.taskName, taskDescription: this.task.taskDescription }), {
          headers: {
            'Content-Type': 'application/json'
          }
        })
      } catch (error) {
        this.errorMsg = 'Error geting data'
      }
      this.getGenerated()
      this.task = null
    }
  }
}
</script>

<style lang="scss"></style>
