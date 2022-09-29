<template>
  <div>
    <v-card outlined>
      <v-card-title>
        Flashcards, Fiszki   <!-- {{ items }} -->
        <!-- <v-btn color="success" small class="mx-1" @click="getGenerated()">Odśwież</v-btn> -->
      </v-card-title>
    </v-card>
    <v-card v-if="showTable" outlined>
      <v-card-title>
        <v-btn
          color="primary"
          @click="editItem('00000000-0000-0000-0000-000000000000')"
        >
          Dodaj Kurs
        </v-btn>
        <v-spacer />
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
            @click.stop="editCard('00000000-0000-0000-0000-000000000000', item.id)"
          >
            <v-icon small>
              mdi-calendar-check
            </v-icon>
            Dodaj fiszkę
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
          <v-btn color="error" small class="mx-1" @click="deleteCourse(item.id)">
            <v-icon small>
              mdi-delete-forever
            </v-icon>
            Usuń !
          </v-btn>
        </template>
        <template #expanded-item="{ headers, item }">
          <td :colspan="headers.length">
            <course-detail :item="item" @editCard="editCard" @updateCard="updateCard" @deleteCard="deleteCard" />
          </td>
        </template>
      </v-data-table>
      <v-dialog v-model="showEditor" max-width="600">
        <course-editor
          :id="itemId"
          :key="itemId"
          @cancel="cancelItem"
          @save="saveCourse"
          @deleteCourse="deleteCourse"
        />
      </v-dialog>
      <v-dialog v-model="showCardEditor" max-width="600">
        <card-editor
          :id="cardId"
          :key="cardId"
          @cancel="cancelCard"
          @save="saveCard"
          @deleteCard="deleteCard"
        />
      </v-dialog>
    </v-card>
  </div>
</template>

<script>
import CourseDetail from '~/components/CourseDetail.vue'
export default {
  components: { CourseDetail },
  data: () => ({
    headers: [
      { value: 'name', text: 'Nazwa', sortable: true },
      { value: 'description', text: 'Opis', sortable: true },
      { value: 'number', text: 'Liczba', sortable: true },
      { value: 'id', text: 'Operacje', sortable: false },
      { text: 'Fiszki', value: 'data-table-expand' }
    ],
    items: [],
    name: '',
    description: '',
    card: { listId: null, cardName: null, cardDescription: null },
    options: {},
    listOfcards: {},
    showEditor: false,
    showCardEditor: false,
    expanded: [],
    search: '',
    componentKey: 0,
    componentKeycard: 0,
    confirmdel: false,
    showTable: true,
    itemId: null,
    cardId: null,
    cardListId: null
  }),
  async fetch () {
    await this.getGenerated()
  },
  methods: {
    async getGenerated () {
      await this.$axios.get('/api/Courses/')
        .then((response) => {
          this.items = response.data

          // this.refactorColumn()
        })
        .catch((error) => {
          console.log(error)
          this.errorMsg = 'Error geting data'
        })
    },
    // refactorColumn () {
    //   for (let i = 0; i < this.items.length; i++) {
    //     this.items[i].czasGenModForRead = new Date(this.items[i].created).toISOString().slice(0, 10) + ' ' + new Date(this.items[i].created).getHours() + ':' + new Date(this.items[i].created).getMinutes()
    //   }
    // },
    editItem (id) {
      this.itemId = id
      this.showEditor = true
    },
    editCard (id, cardListId) {
      this.cardId = id
      this.cardListId = cardListId
      this.showCardEditor = true
    },
    cancelItem () {
      this.itemId = null
      this.cardListId = null
      this.showEditor = false
    },
    cancelCard () {
      this.cardId = null
      this.showCardEditor = false
    },
    async saveCourse (item) {
      if (item.id === '00000000-0000-0000-0000-000000000000') {
        try {
          await this.$axios.$post('/api/Courses/', JSON.stringify({ name: item.name, description: item.description }), {
            headers: {
              'Content-Type': 'application/json'
            }
          })
        } catch (error) {
          this.errorMsg = 'Error posting data'
        }
      } else {
        try {
          await this.$axios.$put('/api/Courses/', JSON.stringify({ id: item.id, name: item.name, description: item.description }), {
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
    async saveCard (item) {
      if (item.id === '00000000-0000-0000-0000-000000000000') {
        try {
          await this.$axios.$post('/api/Courses/', JSON.stringify({ id: item.id, listId: this.cardListId, name: item.name, description: item.description }), {
            headers: {
              'Content-Type': 'application/json'
            }
          })
        } catch (error) {
          this.errorMsg = 'Error geting data'
        }
      } else {
        try {
          await this.$axios.$put('/api/Courses/', JSON.stringify({ id: item.id, name: item.name, description: item.description }), {
            headers: {
              'Content-Type': 'application/json'
            }
          })
        } catch (error) {
          this.errorMsg = 'Error putting data'
        }
      }

      this.showCardEditor = false
      this.cardId = null
      this.$fetch()
    },
    async deleteCourse (id) {
      try {
        await this.$axios.$delete(`/api/Courses/${id}`)
      } catch (error) {
        this.errorMsg = 'Error putting data'
      }
      this.showEditor = false
      this.$fetch()
    },
    async deleteCard (id, listId) {
      try {
        await this.$axios.$delete('/api/cardFromList/',
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
    async updateCard (id, name, description, completed) {
      try {
        await this.$axios.$put('/api/cardFromList/', JSON.stringify({ id, name, description, completed }), {
          headers: {
            'Content-Type': 'application/json'
          }
        })
      } catch (error) {
        this.errorMsg = 'Error geting data'
      }
    },
    async addcard () {
      try {
        await this.$axios.$post('/api/ListOfcards/card', JSON.stringify({ listId: this.card.listId, cardName: this.card.cardName, cardDescription: this.card.cardDescription }), {
          headers: {
            'Content-Type': 'application/json'
          }
        })
      } catch (error) {
        this.errorMsg = 'Error geting data'
      }
      this.getGenerated()
      this.card = null
    }
  }
}
</script>

<style lang="scss"></style>
