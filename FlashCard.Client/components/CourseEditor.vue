<template>
  <div>
    <v-card max-width="600" class="mx-auto grey darken-3">
      <v-card-title class="text-h5 black justify-center">
        {{ course.id === '00000000-0000-0000-0000-000000000000' ? 'Nowa kurs:' : 'Edycja kursu:' }}
        <!-- {{ course }} -->
      </v-card-title>
      <v-form>
        <v-card-text>
          <div class="d-flex flex-column flex-sm-row">
            <v-text-field
              v-model="course.name"
              label="Nazwa kursu"
              outlined
              dense
              class="mx-1"
            />
          </div>
          <div class="d-flex flex-column flex-sm-row">
            <v-text-field
              v-model="course.description"
              label="Opis kursu"
              outlined
              dense
              class="mx-1"
            />
          </div>
          <v-card-actions>
            <v-btn @click.stop="$emit('cancel')">
              Anuluj
            </v-btn>
            <v-spacer />
            <v-btn
              color="primary"
              @click.stop="$emit('save', course)"
            >
              Zapisz
            </v-btn>
          </v-card-actions>
        </v-card-text>
      </v-form>
    </v-card>
  </div>
</template>

<script>
export default {
  props: ['id'],
  data () {
    return {
      course: { id: '00000000-0000-0000-0000-000000000000', name: '', description: '' },
      confirmdel: false,
      orgName: ''
    }
  },
  fetch () {
    console.log(this.id)
    if (
      this.id !== '00000000-0000-0000-0000-000000000000' &&
      this.id !== null // bo problem 2 edycja
    ) {
      this.$axios.get(`/api/Courses/${this.id}`)
        .then((response) => {
          this.course = response.data

          // this.refactorColumn()
        })
        .catch((error) => {
          console.log(error)
          this.errorMsg = 'Error geting data'
        })
      // const response = await this.$countryApi.getById(this.id)
      // this.item = response.json
      this.orgName = this.name
    }
  },
  methods: {
    firstLetterToUpper (name) {
      return name.charAt(0).toUpperCase() + name.substring(1)
    }
  }
}
</script>
<style lang="scss" scoped></style>
