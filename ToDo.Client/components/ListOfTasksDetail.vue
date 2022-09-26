<template>
  <div v-if="item">
    <h5>Lista tasków:</h5>
    <ul id="v-for-object" style="list-style-type:none;">
      <li v-for="(value, name) in item.tasksFromList" :key="name">
        <h3>Nazwa: {{ value.name }} </h3> <h4><br> {{ value.description ? 'Opis: ' + value.description : value.description }} </h4><br>
        Dodano: {{ new Date(value.created).toISOString().slice(0, 10) + ' ' + new Date(value.created).getHours() + ':' + new Date(value.created).getMinutes() }}
        <v-checkbox
          v-model="value.completed"
          label="Zrobione"
          @change="$emit('updateTask',value.id, value.name, value.description, value.completed)"
        />
        <v-btn color="primary" small class="mx-1" @click.stop="$emit('editTask',value.id, item.id)">
          <v-icon small>
            mdi-delete-forever
          </v-icon>
          Zmień
        </v-btn>
        <v-btn color="error" small class="mx-1" @click.stop="$emit('deleteTask',value.id, item.id)">
          <v-icon small>
            mdi-delete-forever
          </v-icon>
          Usuń !
        </v-btn>
        <br><br>
        <v-divider><br></v-divider>
      </li>
    </ul>
  </div>
</template>
<script>
export default {
  props: {
    item: {
      type: Object
    }
  },
  data: () => ({
  }),
  methods: {
  }
}
</script>
